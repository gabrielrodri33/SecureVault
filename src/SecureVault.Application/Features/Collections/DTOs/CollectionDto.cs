namespace SecureVault.Application.Features.Collections.DTOs;
public record CollectionDto(Guid Id, string Name, string? Description, int ItemCount, DateTime CreatedAt);
