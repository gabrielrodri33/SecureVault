using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Exceptions;
using SecureVault.Application.Common.Interfaces;
namespace SecureVault.Application.Features.Vault.Commands.DeleteVaultItem;
public class DeleteVaultItemCommandHandler(IApplicationDbContext db, ICurrentUserService currentUser) : IRequestHandler<DeleteVaultItemCommand, Unit>
{
    public async Task<Unit> Handle(DeleteVaultItemCommand request, CancellationToken cancellationToken)
    {
        var item = await db.VaultItems.FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.VaultItem), request.Id);
        if (item.UserId != currentUser.UserId) throw new ForbiddenException();
        db.VaultItems.Remove(item);
        await db.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
