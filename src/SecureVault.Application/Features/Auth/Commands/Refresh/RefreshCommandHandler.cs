using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Exceptions;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Auth.DTOs;
using SecureVault.Domain.Entities;
namespace SecureVault.Application.Features.Auth.Commands.Refresh;
public class RefreshCommandHandler(IApplicationDbContext db, IJwtService jwt) : IRequestHandler<RefreshCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var token = await db.RefreshTokens.Include(r => r.User).FirstOrDefaultAsync(r => r.Token == request.RefreshToken, cancellationToken)
            ?? throw new UnauthorizedException("Invalid refresh token.");
        if (token.IsRevoked || token.ExpiresAt < DateTime.UtcNow) throw new UnauthorizedException("Refresh token expired or revoked.");
        token.IsRevoked = true;
        var newRefreshToken = jwt.GenerateRefreshToken();
        db.RefreshTokens.Add(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = token.UserId,
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow
        });
        await db.SaveChangesAsync(cancellationToken);
        return new AuthResponse(jwt.GenerateAccessToken(token.User), newRefreshToken, new UserDto(token.User.Id, token.User.Email, token.User.Name, token.User.Role.ToString()));
    }
}
