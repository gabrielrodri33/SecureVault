using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Infrastructure.Data;
using SecureVault.Infrastructure.Services;
namespace SecureVault.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = BuildConnectionString(configuration);

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IApplicationDbContext>(p => p.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICryptoService, CryptoService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    NameClaimType = "sub",
                    RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                };
            });
        return services;
    }

    private static string BuildConnectionString(IConfiguration configuration)
    {
        // 1. DATABASE_URL in URI format (postgresql://user:pass@host:port/db)
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        if (!string.IsNullOrEmpty(databaseUrl) &&
            (databaseUrl.StartsWith("postgresql://") || databaseUrl.StartsWith("postgres://")))
        {
            var uri = new Uri(databaseUrl);
            var userInfo = uri.UserInfo.Split(':', 2);
            return $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={Uri.UnescapeDataString(userInfo[1])};SSL Mode=Require;Trust Server Certificate=true";
        }

        // 2. Individual PG* vars (Railway also exposes these)
        var pgHost = Environment.GetEnvironmentVariable("PGHOST");
        if (!string.IsNullOrEmpty(pgHost))
        {
            var pgPort     = Environment.GetEnvironmentVariable("PGPORT")     ?? "5432";
            var pgDb       = Environment.GetEnvironmentVariable("PGDATABASE") ?? "railway";
            var pgUser     = Environment.GetEnvironmentVariable("PGUSER")     ?? "postgres";
            var pgPassword = Environment.GetEnvironmentVariable("PGPASSWORD") ?? "";
            return $"Host={pgHost};Port={pgPort};Database={pgDb};Username={pgUser};Password={pgPassword};SSL Mode=Require;Trust Server Certificate=true";
        }

        // 3. Local / Docker fallback (appsettings.json)
        return configuration.GetConnectionString("DefaultConnection")!;
    }
}
