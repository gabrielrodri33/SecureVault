using MediatR;
using SecureVault.Application.Features.Admin.DTOs;
namespace SecureVault.Application.Features.Admin.Commands.ToggleUserStatus;
public record ToggleUserStatusCommand(Guid Id) : IRequest<AdminUserDto>;
