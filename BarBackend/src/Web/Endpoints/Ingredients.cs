using BarBackend.Application.Common.Models;
using BarBackend.Application.Ingredients.Commands.CreateIngredientItem;
using BarBackend.Application.Ingredients.Commands.DeleteIngredient;
using BarBackend.Application.Ingredients.Commands.UpdateIngredient;
using BarBackend.Application.Ingredients.Queries.GetIngredientsWithPagination;

namespace BarBackend.Web.Endpoints;

public class Ingredients : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetIngredientsWithPagination)
            .MapPost(CreateIngredient)
            .MapPut(UpdateIngredient, "{id}")
            .MapDelete(DeleteIngredient, "{id}");
    }

    public async Task<PaginatedList<IngredientDto>> GetIngredientsWithPagination(ISender sender, [AsParameters] GetIngredientsWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<int> CreateIngredient(ISender sender, CreateIngredientCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateIngredient(ISender sender, int id, UpdateIngredientCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
    
    public async Task<IResult> DeleteIngredient(ISender sender, int id)
    {
        await sender.Send(new DeleteIngredientCommand(id));
        return Results.NoContent();
    }
}
