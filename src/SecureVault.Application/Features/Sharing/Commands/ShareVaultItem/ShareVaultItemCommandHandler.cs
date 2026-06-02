using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Exceptions;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Domain.Entities;
namespace SecureVault.Application.Features.Sharing.Commands.ShareVaultItem;
public class ShareVaultItemCommandHandler(IApplicationDbContext db, ICurrentUserService currentUser) : IRequestHandler<ShareVaultItemCommand, Guid>
{
    public async Task<Guid> Handle(ShareVaultItemCommand request, CancellationToken cancellationToken)
    {
        var item = await db.VaultItems.FirstOrDefaultAsync(v => v.Id == request.VaultItemId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.VaultItem), request.VaultItemId);
        if (item.UserId != currentUser.UserId) throw new ForbiddenException();
        var targetUser = await db.Users.FirstOrDefaultAsync(u => u.Email == request.SharedWithUserEmail.ToLower(), cancellationToken)
            ?? throw new NotFoundException("User", request.SharedWithUserEmail);
        if (await db.SharedItems.AnyAsync(s => s.VaultItemId == request.VaultItemId && s.SharedWithUserId == targetUser.Id, cancellationToken))
            throw new InvalidOperationException("Item already shared with this user.");
        var share = new SharedItem { Id = Guid.NewGuid(), VaultItemId = request.VaultItemId, SharedWithUserId = targetUser.Id, CanEdit = request.CanEdit, CreatedAt = DateTime.UtcNow };
        db.SharedItems.Add(share);
        await db.SaveChangesAsync(cancellationToken);
        return share.Id;
    }
}
