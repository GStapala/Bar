using BarBackend.Application.Ingredients.Queries.GetIngredientsWithPagination;
using BarBackend.Domain.Entities;

namespace BarBackend.Application.Recipes.Queries.GetRecipesWithPagination;

public class RecipeDto
{
    public int Id { get; init; }

    public string? Name { get; init; }

    public string? Description { get; set; }

    public IEnumerable<Ingredient>? Ingredients { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Recipe, RecipeDto>()
            .ForMember(dest => dest.Ingredients, opt =>
                opt.MapFrom(src => src.RecipeIngredients.Select(x => x).Select(x => x.Ingredient)));


            // string.Join(", ", src.RecipeIngredients.Select(ri => ri.Ingredient.Name))));
            // opt.MapFrom(src => src.RecipeIngredients));
        }
    }
}
