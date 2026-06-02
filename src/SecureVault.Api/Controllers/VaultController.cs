using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureVault.Application.Features.Sharing.Commands.DeleteShare;
using SecureVault.Application.Features.Sharing.Commands.ShareVaultItem;
using SecureVault.Application.Features.Sharing.Queries.GetSharedWithMe;
using SecureVault.Application.Features.Vault.Commands.CreateVaultItem;
using SecureVault.Application.Features.Vault.Commands.DeleteVaultItem;
using SecureVault.Application.Features.Vault.Commands.ToggleFavorite;
using SecureVault.Application.Features.Vault.Commands.UpdateVaultItem;
using SecureVault.Application.Features.Vault.Queries.GetFavorites;
using SecureVault.Application.Features.Vault.Queries.GetVaultItem;
using SecureVault.Application.Features.Vault.Queries.GetVaultItems;
namespace SecureVault.Api.Controllers;
[ApiController]
[Route("api/vault")]
[Authorize]
public class VaultController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] Guid? collectionId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        => Ok(await mediator.Send(new GetVaultItemsQuery(search, collectionId, page, pageSize)));

    [HttpGet("favorites")]
    public async Task<IActionResult> GetFavorites() => Ok(await mediator.Send(new GetFavoritesQuery()));

    [HttpGet("shared-with-me")]
    public async Task<IActionResult> GetSharedWithMe() => Ok(await mediator.Send(new GetSharedWithMeQuery()));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await mediator.Send(new GetVaultItemQuery(id)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVaultItemCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVaultItemCommand command)
        => Ok(await mediator.Send(command with { Id = id }));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id) { await mediator.Send(new DeleteVaultItemCommand(id)); return NoContent(); }

    [HttpPatch("{id:guid}/favorite")]
    public async Task<IActionResult> ToggleFavorite(Guid id) { await mediator.Send(new ToggleFavoriteCommand(id)); return NoContent(); }

    [HttpPost("{id:guid}/share")]
    public async Task<IActionResult> Share(Guid id, [FromBody] ShareVaultItemRequest req)
    {
        var shareId = await mediator.Send(new ShareVaultItemCommand(id, req.SharedWithUserEmail, req.CanEdit));
        return CreatedAtAction(nameof(Get), new { id }, new { shareId });
    }

    [HttpDelete("{id:guid}/share/{shareId:guid}")]
    public async Task<IActionResult> DeleteShare(Guid id, Guid shareId) { await mediator.Send(new DeleteShareCommand(id, shareId)); return NoContent(); }
}
public record ShareVaultItemRequest(string SharedWithUserEmail, bool CanEdit);
