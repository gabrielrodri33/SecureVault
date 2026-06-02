using Microsoft.EntityFrameworkCore;
using SecureVault.Domain.Entities;
namespace SecureVault.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<VaultItem> VaultItems { get; }
    DbSet<Collection> Collections { get; }
    DbSet<SharedItem> SharedItems { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
