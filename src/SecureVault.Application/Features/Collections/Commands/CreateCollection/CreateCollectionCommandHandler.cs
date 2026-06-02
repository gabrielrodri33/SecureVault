using MediatR;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Collections.DTOs;
using SecureVault.Domain.Entities;
namespace SecureVault.Application.Features.Collections.Commands.CreateCollection;
public class CreateCollectionCommandHandler(IApplicationDbContext db, ICurrentUserService currentUser) : IRequestHandler<CreateCollectionCommand, CollectionDto>
{
    public async Task<CollectionDto> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
    {
        var col = new Collection { Id = Guid.NewGuid(), UserId = currentUser.UserId, Name = request.Name, Description = request.Description, CreatedAt = DateTime.UtcNow };
        db.Collections.Add(col);
        await db.SaveChangesAsync(cancellationToken);
        return new CollectionDto(col.Id, col.Name, col.Description, 0, col.CreatedAt);
    }
}
