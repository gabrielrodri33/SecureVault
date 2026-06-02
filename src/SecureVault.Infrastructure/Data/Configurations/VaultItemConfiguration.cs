using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecureVault.Domain.Entities;
namespace SecureVault.Infrastructure.Data.Configurations;
public class VaultItemConfiguration : IEntityTypeConfiguration<VaultItem>
{
    public void Configure(EntityTypeBuilder<VaultItem> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Title).IsRequired().HasMaxLength(200);
        builder.Property(v => v.Username).HasMaxLength(200);
        builder.Property(v => v.EncryptedPassword).IsRequired();
        builder.Property(v => v.Url).HasMaxLength(2000);
        builder.HasIndex(v => v.UserId);
        builder.HasIndex(v => new { v.UserId, v.IsFavorite });
        builder.HasMany(v => v.SharedItems).WithOne(s => s.VaultItem).HasForeignKey(s => s.VaultItemId).OnDelete(DeleteBehavior.Cascade);
    }
}
