using BarBackend.Application.Common.Interfaces;
using BarBackend.Domain.Entities;
using BarBackend.Domain.Events;

namespace BarBackend.Application.Recipes.Commands.CreateRecipe;

public record CreateRecipeCommand : IRequest<int>
{
    public required string Name { get; init; }
}

public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, int>
{
    private readonly IBarDbContext _context;

    public CreateRecipeCommandHandler(IBarDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var entity = new Recipe()
        {
            Name = request.Name,
           
        };

        entity.AddDomainEvent(new RecipeCreatedEvent(entity));

        _context.Recipes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
