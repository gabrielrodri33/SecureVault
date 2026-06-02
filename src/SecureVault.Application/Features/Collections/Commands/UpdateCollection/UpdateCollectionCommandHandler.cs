using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Exceptions;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Collections.DTOs;
namespace SecureVault.Application.Features.Collections.Commands.UpdateCollection;
public class UpdateCollectionCommandHandler(IApplicationDbContext db, ICurrentUserService currentUser) : IRequestHandler<UpdateCollectionCommand, CollectionDto>
{
    public async Task<CollectionDto> Handle(UpdateCollectionCommand request, CancellationToken cancellationToken)
    {
        var col = await db.Collections.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Collection), request.Id);
        if (col.UserId != currentUser.UserId) throw new ForbiddenException();
        col.Name = request.Name; col.Description = request.Description;
        await db.SaveChangesAsync(cancellationToken);
        return new CollectionDto(col.Id, col.Name, col.Description, col.Items.Count, col.CreatedAt);
    }
}
