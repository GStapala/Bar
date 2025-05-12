using BarBackend.Application.Common.Interfaces;
using BarBackend.Domain.Events;

namespace BarBackend.Application.Recipes.Commands.DeleteRecipe;

public record DeleteRecipeCommand(int Id) : IRequest;

public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand>
{
    private readonly IBarDbContext _context;

    public DeleteRecipeCommandHandler(IBarDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Recipes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Recipes.Remove(entity);

        entity.AddDomainEvent(new RecipeDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
