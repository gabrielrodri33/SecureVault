namespace SecureVault.Domain.Entities;
public class SharedItem
{
    public Guid Id { get; set; }
    public Guid VaultItemId { get; set; }
    public Guid SharedWithUserId { get; set; }
    public bool CanEdit { get; set; }
    public DateTime CreatedAt { get; set; }
    public VaultItem VaultItem { get; set; } = null!;
    public User SharedWithUser { get; set; } = null!;
}
