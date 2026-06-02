using SecureVault.Domain.Entities;
namespace SecureVault.Application.Common.Interfaces;
public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    Guid? GetUserIdFromToken(string token);
}
