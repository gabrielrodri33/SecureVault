using MediatR;
using SecureVault.Application.Features.Auth.DTOs;
namespace SecureVault.Application.Features.Auth.Commands.Refresh;
public record RefreshCommand(string RefreshToken) : IRequest<AuthResponse>;
