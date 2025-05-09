using BarBackend.Application.Categories.Commands.CreateCategory;
using BarBackend.Application.Common.Models;
using BarBackend.Application.Categories.Commands.DeleteCategory;
using BarBackend.Application.Categories.Commands.UpdateCategory;
using BarBackend.Application.Categories.Queries.GetCategoriesWithPagination;

namespace BarBackend.Web.Endpoints;

public class Categories : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetCategoriesWithPagination)
            .MapGet(GetSubCategoriesWithPagination, "{id}/subcategories")
            .MapPost(CreateCategory)
            .MapPut(UpdateCategory, "{id}")
            .MapDelete(DeleteCategory, "{id}");
    }

    public async Task<PaginatedList<CategoryDto>> GetCategoriesWithPagination(ISender sender, [AsParameters] GetCategoriesWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<int> CreateCategory(ISender sender, CreateCategoryCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateCategory(ISender sender, int id, UpdateCategoryCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteCategory(ISender sender, int id)
    {
        await sender.Send(new DeleteCategoryCommand(id));
        return Results.NoContent();
    }

    public async Task<IResult> GetSubCategoriesWithPagination(ISender sender, int id, [AsParameters] GetSubCategoriesWithPaginationQuery query)
    {
        if (id != query.ParentCategoryId) return Results.BadRequest("ParentCategoryId does not match the provided id.");
        var result = await sender.Send(query);
        return Results.Ok(result);
    }
}
