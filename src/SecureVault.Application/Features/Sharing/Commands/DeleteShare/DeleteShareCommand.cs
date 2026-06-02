using MediatR;
namespace SecureVault.Application.Features.Sharing.Commands.DeleteShare;
public record DeleteShareCommand(Guid VaultItemId, Guid ShareId) : IRequest<Unit>;
