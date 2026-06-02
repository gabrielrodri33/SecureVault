using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Exceptions;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Auth.DTOs;
using SecureVault.Domain.Entities;
namespace SecureVault.Application.Features.Auth.Commands.Login;
public class LoginCommandHandler(IApplicationDbContext db, IJwtService jwt, ICryptoService crypto) : IRequestHandler<LoginCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == request.Email.ToLower(), cancellationToken)
            ?? throw new UnauthorizedException("Invalid email or password.");
        if (!user.IsActive) throw new UnauthorizedException("Account is disabled.");
        if (!crypto.VerifyPassword(request.Password, user.PasswordHash)) throw new UnauthorizedException("Invalid email or password.");
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
