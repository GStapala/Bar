using BarBackend.Application.Common.Interfaces;
using BarBackend.Domain.Entities;
using BarBackend.Domain.Enums;
using BarBackend.Domain.Events;

namespace BarBackend.Application.Ingredients.Commands.CreateIngredient;

public record CreateIngredientCommand : IRequest<int>
{
    public string? Name { get; init; }
    public MeasurementType? MeasurementType { get; init; } // ml, g, pcs, etc.
    public decimal MeasurementValue { get; init; }
    public string? Description { get; init; }
    public int StockQuantity { get; init; }
    public int CategoryId { get; init; }

}

public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, int>
{
    private readonly IBarDbContext _context;

    public CreateIngredientCommandHandler(IBarDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        var entity = new Ingredient()
        {
            Name = request.Name,
            MeasurementType = request.MeasurementType,
            MeasurementValue = request.MeasurementValue,
            Description = request.Description,
            StockQuantity = request.StockQuantity,
            CategoryId = request.CategoryId,
        };

        entity.AddDomainEvent(new IngredientCreatedEvent(entity));

        _context.Ingredients.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
