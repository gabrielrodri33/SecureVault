using MediatR;
using SecureVault.Application.Features.Vault.DTOs;
namespace SecureVault.Application.Features.Collections.Queries.GetCollectionItems;
public record GetCollectionItemsQuery(Guid CollectionId) : IRequest<IList<VaultItemDto>>;
