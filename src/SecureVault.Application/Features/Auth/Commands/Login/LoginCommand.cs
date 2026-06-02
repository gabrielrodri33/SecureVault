using MediatR;
using SecureVault.Application.Features.Auth.DTOs;
namespace SecureVault.Application.Features.Auth.Commands.Login;
public record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;
