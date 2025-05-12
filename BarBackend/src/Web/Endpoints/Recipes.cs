using BarBackend.Application.Recipes.Commands.CreateRecipe;
using BarBackend.Application.Common.Models;
using BarBackend.Application.Recipes.Commands.DeleteRecipe;
using BarBackend.Application.Recipes.Commands.UpdateRecipe;
using BarBackend.Application.Recipes.Queries.GetRecipesWithPagination;

namespace BarBackend.Web.Endpoints;

public class Recipes : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetRecipesWithPagination)
            .MapPost(CreateRecipe)
            .MapPut(UpdateRecipe, "{id}")
            .MapDelete(DeleteRecipe, "{id}");
    }

    public async Task<PaginatedList<RecipeDto>> GetRecipesWithPagination(ISender sender, [AsParameters] GetRecipesWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<int> CreateRecipe(ISender sender, CreateRecipeCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateRecipe(ISender sender, int id, UpdateRecipeCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteRecipe(ISender sender, int id)
    {
        await sender.Send(new DeleteRecipeCommand(id));
        return Results.NoContent();
    }
}
