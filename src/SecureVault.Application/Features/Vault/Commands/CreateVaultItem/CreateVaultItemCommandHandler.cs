using MediatR;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Vault.DTOs;
using SecureVault.Domain.Entities;
namespace SecureVault.Application.Features.Vault.Commands.CreateVaultItem;
public class CreateVaultItemCommandHandler(IApplicationDbContext db, ICurrentUserService currentUser, ICryptoService crypto) : IRequestHandler<CreateVaultItemCommand, VaultItemDto>
{
    public async Task<VaultItemDto> Handle(CreateVaultItemCommand request, CancellationToken cancellationToken)
    {
        var item = new VaultItem
        {
            Id = Guid.NewGuid(),
            UserId = currentUser.UserId,
            Title = request.Title,
            Username = request.Username,
            EncryptedPassword = crypto.Encrypt(request.Password),
            Url = request.Url,
            Notes = request.Notes,
            CollectionId = request.CollectionId,
            IsFavorite = request.IsFavorite,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        db.VaultItems.Add(item);
        await db.SaveChangesAsync(cancellationToken);
        return new VaultItemDto(item.Id, item.Title, item.Username, item.Url, item.IsFavorite, item.CollectionId, item.CreatedAt, item.UpdatedAt);
    }
}
