using MediatR;
using SecureVault.Application.Features.Collections.DTOs;
namespace SecureVault.Application.Features.Collections.Commands.UpdateCollection;
public record UpdateCollectionCommand(Guid Id, string Name, string? Description) : IRequest<CollectionDto>;
