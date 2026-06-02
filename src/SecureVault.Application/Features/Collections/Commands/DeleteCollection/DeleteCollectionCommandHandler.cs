using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Exceptions;
using SecureVault.Application.Common.Interfaces;
namespace SecureVault.Application.Features.Collections.Commands.DeleteCollection;
public class DeleteCollectionCommandHandler(IApplicationDbContext db, ICurrentUserService currentUser) : IRequestHandler<DeleteCollectionCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
    {
        var col = await db.Collections.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Collection), request.Id);
        if (col.UserId != currentUser.UserId) throw new ForbiddenException();
        db.Collections.Remove(col);
        await db.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
