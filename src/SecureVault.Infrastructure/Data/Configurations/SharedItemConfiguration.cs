using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecureVault.Domain.Entities;
namespace SecureVault.Infrastructure.Data.Configurations;
public class SharedItemConfiguration : IEntityTypeConfiguration<SharedItem>
{
    public void Configure(EntityTypeBuilder<SharedItem> builder)
    {
        builder.HasKey(s => s.Id);
        builder.HasIndex(s => new { s.VaultItemId, s.SharedWithUserId }).IsUnique();
    }
}
