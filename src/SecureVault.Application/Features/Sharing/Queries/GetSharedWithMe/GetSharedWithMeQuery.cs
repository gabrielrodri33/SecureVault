using MediatR;
using SecureVault.Application.Features.Sharing.DTOs;
namespace SecureVault.Application.Features.Sharing.Queries.GetSharedWithMe;
public record GetSharedWithMeQuery() : IRequest<IList<SharedVaultItemDto>>;
