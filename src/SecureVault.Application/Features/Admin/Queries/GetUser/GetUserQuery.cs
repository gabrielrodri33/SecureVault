using MediatR;
using SecureVault.Application.Features.Admin.DTOs;
namespace SecureVault.Application.Features.Admin.Queries.GetUser;
public record GetUserQuery(Guid Id) : IRequest<AdminUserDto>;
