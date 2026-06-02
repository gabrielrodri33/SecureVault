namespace SecureVault.Domain.Entities;
public class VaultItem
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? CollectionId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string EncryptedPassword { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string? Notes { get; set; }
    public bool IsFavorite { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public User User { get; set; } = null!;
    public Collection? Collection { get; set; }
    public ICollection<SharedItem> SharedItems { get; set; } = [];
}
