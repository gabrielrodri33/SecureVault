using MediatR;
using SecureVault.Application.Features.Collections.DTOs;
namespace SecureVault.Application.Features.Collections.Commands.CreateCollection;
public record CreateCollectionCommand(string Name, string? Description) : IRequest<CollectionDto>;
