using System.Net;
using System.Text.Json;
using FluentValidation;
using SecureVault.Application.Common.Exceptions;
namespace SecureVault.Api.Middleware;
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try { await next(context); }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/problem+json";
        var (statusCode, title, errors) = exception switch
        {
            ValidationException ve => (HttpStatusCode.BadRequest, "Validation failed",
                ve.Errors.GroupBy(e => e.PropertyName).ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray())),
            NotFoundException => (HttpStatusCode.NotFound, exception.Message, (Dictionary<string, string[]>?)null),
            UnauthorizedException => (HttpStatusCode.Unauthorized, exception.Message, null),
            ForbiddenException => (HttpStatusCode.Forbidden, exception.Message, null),
            InvalidOperationException => (HttpStatusCode.BadRequest, exception.Message, null),
            _ => (HttpStatusCode.InternalServerError, "An error occurred.", null)
        };
        context.Response.StatusCode = (int)statusCode;
        var problem = new { type = $"https://httpstatuses.com/{(int)statusCode}", title, status = (int)statusCode, errors };
        await context.Response.WriteAsync(JsonSerializer.Serialize(problem, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
    }
}
