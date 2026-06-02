namespace SecureVault.Application.Features.Vault.DTOs;
public record VaultItemDto(Guid Id, string Title, string? Username, string? Url, bool IsFavorite, Guid? CollectionId, DateTime CreatedAt, DateTime UpdatedAt);
public record VaultItemDetailDto(Guid Id, string Title, string? Username, string? Url, bool IsFavorite, Guid? CollectionId, string Password, string? Notes, DateTime CreatedAt, DateTime UpdatedAt);
public record PaginatedResult<T>(IList<T> Items, int TotalCount, int Page, int PageSize);
