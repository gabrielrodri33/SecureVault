using MediatR;
using SecureVault.Application.Features.Auth.DTOs;
namespace SecureVault.Application.Features.Auth.Commands.Register;
public record RegisterCommand(string Email, string Password, string Name) : IRequest<AuthResponse>;
