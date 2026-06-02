using MediatR;
using SecureVault.Application.Features.Collections.DTOs;
namespace SecureVault.Application.Features.Collections.Queries.GetCollections;
public record GetCollectionsQuery() : IRequest<IList<CollectionDto>>;
