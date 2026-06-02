using MediatR;
namespace SecureVault.Application.Features.Vault.Commands.ToggleFavorite;
public record ToggleFavoriteCommand(Guid Id) : IRequest<Unit>;
