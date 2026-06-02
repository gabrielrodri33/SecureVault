using MediatR;
using SecureVault.Application.Features.Admin.DTOs;
namespace SecureVault.Application.Features.Admin.Queries.GetUsers;
public record GetUsersQuery() : IRequest<IList<AdminUserDto>>;
