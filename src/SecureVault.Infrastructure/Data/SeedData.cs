using SecureVault.Application.Common.Interfaces;
using SecureVault.Domain.Entities;
using SecureVault.Domain.Enums;
using Microsoft.EntityFrameworkCore;
namespace SecureVault.Infrastructure.Data;
public static class SeedData
{
    public static async Task SeedAsync(ApplicationDbContext context, ICryptoService crypto)
    {
        if (await context.Users.AnyAsync(u => u.Role == UserRole.Admin)) return;
        var admin = new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@securevault.com",
            PasswordHash = crypto.HashPassword("Admin@123"),
            Name = "Admin",
            Role = UserRole.Admin,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        context.Users.Add(admin);
        await context.SaveChangesAsync();
    }
}
