using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureVault.Application.Features.Admin.Commands.ToggleUserStatus;
using SecureVault.Application.Features.Admin.Queries.GetStats;
using SecureVault.Application.Features.Admin.Queries.GetUser;
using SecureVault.Application.Features.Admin.Queries.GetUsers;
namespace SecureVault.Api.Controllers;
[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController(IMediator mediator) : ControllerBase
{
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers() => Ok(await mediator.Send(new GetUsersQuery()));

    [HttpGet("users/{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id) => Ok(await mediator.Send(new GetUserQuery(id)));

    [HttpPatch("users/{id:guid}/status")]
    public async Task<IActionResult> ToggleStatus(Guid id) => Ok(await mediator.Send(new ToggleUserStatusCommand(id)));

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats() => Ok(await mediator.Send(new GetStatsQuery()));
}
