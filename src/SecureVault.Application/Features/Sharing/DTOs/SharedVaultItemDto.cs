using SecureVault.Application.Features.Auth.DTOs;
namespace SecureVault.Application.Features.Sharing.DTOs;
public record SharedVaultItemDto(Guid ShareId, Guid ItemId, string Title, string? Username, string? Url, bool IsFavorite, UserDto SharedBy, bool CanEdit, DateTime SharedAt);
