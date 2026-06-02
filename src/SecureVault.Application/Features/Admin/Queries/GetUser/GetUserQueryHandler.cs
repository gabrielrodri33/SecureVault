using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Exceptions;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Admin.DTOs;
namespace SecureVault.Application.Features.Admin.Queries.GetUser;
public class GetUserQueryHandler(IApplicationDbContext db) : IRequestHandler<GetUserQuery, AdminUserDto>
{
    public async Task<AdminUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var u = await db.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.User), request.Id);
        return new AdminUserDto(u.Id, u.Email, u.Name, u.Role.ToString(), u.IsActive, u.CreatedAt);
    }
}
