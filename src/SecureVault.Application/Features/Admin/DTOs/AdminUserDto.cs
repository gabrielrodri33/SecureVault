namespace SecureVault.Application.Features.Admin.DTOs;
public record AdminUserDto(Guid Id, string Email, string Name, string Role, bool IsActive, DateTime CreatedAt);
public record StatsDto(int TotalUsers, int TotalItems, int TotalCollections);
