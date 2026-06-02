using MediatR;
namespace SecureVault.Application.Features.Auth.Commands.Logout;
public record LogoutCommand(string RefreshToken) : IRequest<Unit>;
