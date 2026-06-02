using MediatR;
namespace SecureVault.Application.Features.Collections.Commands.DeleteCollection;
public record DeleteCollectionCommand(Guid Id) : IRequest<Unit>;
