using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Exceptions;
using SecureVault.Application.Common.Interfaces;
namespace SecureVault.Application.Features.Vault.Commands.ToggleFavorite;
public class ToggleFavoriteCommandHandler(IApplicationDbContext db, ICurrentUserService currentUser) : IRequestHandler<ToggleFavoriteCommand, Unit>
{
    public async Task<Unit> Handle(ToggleFavoriteCommand request, CancellationToken cancellationToken)
    {
        var item = await db.VaultItems.FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.VaultItem), request.Id);
        if (item.UserId != currentUser.UserId) throw new ForbiddenException();
        item.IsFavorite = !item.IsFavorite;
        item.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
