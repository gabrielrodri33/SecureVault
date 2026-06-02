using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SecureVault.Application.Common.Interfaces;
namespace SecureVault.Infrastructure.Services;
public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid UserId
    {
        get
        {
            var value = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? httpContextAccessor.HttpContext?.User.FindFirstValue("sub");
            return Guid.TryParse(value, out var id) ? id : Guid.Empty;
        }
    }
    public string Email => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
    public bool IsAdmin => httpContextAccessor.HttpContext?.User.IsInRole("Admin") ?? false;
}
