using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureVault.Application.Features.Auth.Commands.Login;
using SecureVault.Application.Features.Auth.Commands.Logout;
using SecureVault.Application.Features.Auth.Commands.Refresh;
using SecureVault.Application.Features.Auth.Commands.Register;
namespace SecureVault.Api.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command) => Ok(await mediator.Send(command));

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand command) => Ok(await mediator.Send(command));

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh([FromBody] RefreshCommand command) => Ok(await mediator.Send(command));

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command) { await mediator.Send(command); return NoContent(); }
}
