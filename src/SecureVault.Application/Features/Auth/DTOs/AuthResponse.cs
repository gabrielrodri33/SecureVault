namespace SecureVault.Application.Features.Auth.DTOs;
public record AuthResponse(string AccessToken, string RefreshToken, UserDto User);
public record UserDto(Guid Id, string Email, string Name, string Role);
