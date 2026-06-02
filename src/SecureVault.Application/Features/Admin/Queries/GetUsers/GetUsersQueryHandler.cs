using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Admin.DTOs;
namespace SecureVault.Application.Features.Admin.Queries.GetUsers;
public class GetUsersQueryHandler(IApplicationDbContext db) : IRequestHandler<GetUsersQuery, IList<AdminUserDto>>
{
    public async Task<IList<AdminUserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await db.Users.OrderBy(u => u.Email)
            .Select(u => new AdminUserDto(u.Id, u.Email, u.Name, u.Role.ToString(), u.IsActive, u.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}
