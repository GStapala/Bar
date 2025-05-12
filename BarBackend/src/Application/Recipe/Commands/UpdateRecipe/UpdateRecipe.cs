using BarBackend.Application.Common.Interfaces;

namespace BarBackend.Application.Recipes.Commands.UpdateRecipe;

public record UpdateRecipeCommand : IRequest
{
    public int Id { get; init; }

    public required string Name { get; init; }

}

public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand>
{
    private readonly IBarDbContext _context;

    public UpdateRecipeCommandHandler(IBarDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Recipes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
