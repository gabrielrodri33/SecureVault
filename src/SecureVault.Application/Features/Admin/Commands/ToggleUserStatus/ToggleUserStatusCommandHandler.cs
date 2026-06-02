using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Exceptions;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Admin.DTOs;
namespace SecureVault.Application.Features.Admin.Commands.ToggleUserStatus;
public class ToggleUserStatusCommandHandler(IApplicationDbContext db) : IRequestHandler<ToggleUserStatusCommand, AdminUserDto>
{
    public async Task<AdminUserDto> Handle(ToggleUserStatusCommand request, CancellationToken cancellationToken)
    {
        var u = await db.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.User), request.Id);
        u.IsActive = !u.IsActive;
        u.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
        return new AdminUserDto(u.Id, u.Email, u.Name, u.Role.ToString(), u.IsActive, u.CreatedAt);
    }
}
