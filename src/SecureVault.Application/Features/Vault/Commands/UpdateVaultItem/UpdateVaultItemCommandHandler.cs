using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Exceptions;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Vault.DTOs;
namespace SecureVault.Application.Features.Vault.Commands.UpdateVaultItem;
public class UpdateVaultItemCommandHandler(IApplicationDbContext db, ICurrentUserService currentUser, ICryptoService crypto) : IRequestHandler<UpdateVaultItemCommand, VaultItemDto>
{
    public async Task<VaultItemDto> Handle(UpdateVaultItemCommand request, CancellationToken cancellationToken)
    {
        var item = await db.VaultItems.FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.VaultItem), request.Id);
        if (item.UserId != currentUser.UserId) throw new ForbiddenException();
        item.Title = request.Title;
        item.Username = request.Username;
        item.EncryptedPassword = crypto.Encrypt(request.Password);
        item.Url = request.Url;
        item.Notes = request.Notes;
        item.CollectionId = request.CollectionId;
        item.IsFavorite = request.IsFavorite;
        item.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
        return new VaultItemDto(item.Id, item.Title, item.Username, item.Url, item.IsFavorite, item.CollectionId, item.CreatedAt, item.UpdatedAt);
    }
}
