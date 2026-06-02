using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Auth.DTOs;
using SecureVault.Domain.Entities;
namespace SecureVault.Application.Features.Auth.Commands.Register;
public class RegisterCommandHandler(IApplicationDbContext db, IJwtService jwt, ICryptoService crypto) : IRequestHandler<RegisterCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await db.Users.AnyAsync(u => u.Email == request.Email.ToLower(), cancellationToken))
            throw new InvalidOperationException("Email already registered.");
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email.ToLower(),
            PasswordHash = crypto.HashPassword(request.Password),
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        db.Users.Add(user);
        var refreshTokenValue = jwt.GenerateRefreshToken();
        db.RefreshTokens.Add(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshTokenValue,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow
        });
        await db.SaveChangesAsync(cancellationToken);
        return new AuthResponse(jwt.GenerateAccessToken(user), refreshTokenValue, new UserDto(user.Id, user.Email, user.Name, user.Role.ToString()));
    }
}
