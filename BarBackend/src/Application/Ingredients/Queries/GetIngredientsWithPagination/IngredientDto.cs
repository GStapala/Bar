using BarBackend.Domain.Entities;

namespace BarBackend.Application.Ingredients.Queries.GetIngredientsWithPagination;

public class IngredientDto
{
    public int Id { get; init; }

    public int CategoryId { get; init; }

    public string? Name { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Ingredient, IngredientDto>();
        }
    }
}
