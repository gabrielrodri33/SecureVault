using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureVault.Application.Features.Collections.Commands.CreateCollection;
using SecureVault.Application.Features.Collections.Commands.DeleteCollection;
using SecureVault.Application.Features.Collections.Commands.UpdateCollection;
using SecureVault.Application.Features.Collections.Queries.GetCollectionItems;
using SecureVault.Application.Features.Collections.Queries.GetCollections;
namespace SecureVault.Api.Controllers;
[ApiController]
[Route("api/collections")]
[Authorize]
public class CollectionsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await mediator.Send(new GetCollectionsQuery()));

    [HttpGet("{id:guid}/items")]
    public async Task<IActionResult> GetItems(Guid id) => Ok(await mediator.Send(new GetCollectionItemsQuery(id)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCollectionCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCollectionCommand command)
        => Ok(await mediator.Send(command with { Id = id }));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id) { await mediator.Send(new DeleteCollectionCommand(id)); return NoContent(); }
}
