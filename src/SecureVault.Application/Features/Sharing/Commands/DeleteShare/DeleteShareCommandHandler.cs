using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Exceptions;
using SecureVault.Application.Common.Interfaces;
namespace SecureVault.Application.Features.Sharing.Commands.DeleteShare;
public class DeleteShareCommandHandler(IApplicationDbContext db, ICurrentUserService currentUser) : IRequestHandler<DeleteShareCommand, Unit>
{
    public async Task<Unit> Handle(DeleteShareCommand request, CancellationToken cancellationToken)
    {
        var item = await db.VaultItems.FirstOrDefaultAsync(v => v.Id == request.VaultItemId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.VaultItem), request.VaultItemId);
        if (item.UserId != currentUser.UserId) throw new ForbiddenException();
        var share = await db.SharedItems.FirstOrDefaultAsync(s => s.Id == request.ShareId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.SharedItem), request.ShareId);
        db.SharedItems.Remove(share);
        await db.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
