using BarBackend.Application.Common.Interfaces;
using BarBackend.Application.Common.Mappings;
using BarBackend.Application.Common.Models;

namespace BarBackend.Application.Recipes.Queries.GetRecipesWithPagination;

public record GetRecipesWithPaginationQuery : IRequest<PaginatedList<RecipeDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetRecipesWithPaginationQueryHandler : IRequestHandler<GetRecipesWithPaginationQuery, PaginatedList<RecipeDto>>
{
    private readonly IBarDbContext _context;
    private readonly IMapper _mapper;

    public GetRecipesWithPaginationQueryHandler(IBarDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RecipeDto>> Handle(GetRecipesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var recipes = await _context.Recipes
            .Include(x=>x.RecipeIngredients)
            .ThenInclude(x=>x.Ingredient)
            .OrderBy(x => x.Name)
            .ProjectTo<RecipeDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        
        return recipes;
    }
}
