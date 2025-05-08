using BarBackend.Application.Common.Interfaces;

namespace BarBackend.Application.Ingredients.Commands.UpdateIngredient;

public record UpdateIngredientCommand : IRequest
{
    public int Id { get; init; }

    public string? Name { get; init; }

}

public class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommand>
{
    private readonly IBarDbContext _context;

    public UpdateIngredientCommandHandler(IBarDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Ingredients
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
