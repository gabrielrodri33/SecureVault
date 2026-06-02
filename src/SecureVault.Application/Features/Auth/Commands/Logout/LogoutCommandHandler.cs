using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Interfaces;
namespace SecureVault.Application.Features.Auth.Commands.Logout;
public class LogoutCommandHandler(IApplicationDbContext db) : IRequestHandler<LogoutCommand, Unit>
{
    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var token = await db.RefreshTokens.FirstOrDefaultAsync(r => r.Token == request.RefreshToken, cancellationToken);
        if (token != null) { token.IsRevoked = true; await db.SaveChangesAsync(cancellationToken); }
        return Unit.Value;
    }
}
