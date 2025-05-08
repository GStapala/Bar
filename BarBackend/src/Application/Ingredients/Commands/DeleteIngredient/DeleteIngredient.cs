using BarBackend.Application.Common.Interfaces;
using BarBackend.Domain.Events;

namespace BarBackend.Application.Ingredients.Commands.DeleteIngredient;

public record DeleteIngredientCommand(int Id) : IRequest;

public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand>
{
    private readonly IBarDbContext _context;

    public DeleteIngredientCommandHandler(IBarDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Ingredients
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Ingredients.Remove(entity);

        entity.AddDomainEvent(new IngredientDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
