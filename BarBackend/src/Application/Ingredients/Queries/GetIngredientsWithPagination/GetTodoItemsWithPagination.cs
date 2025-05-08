using BarBackend.Application.Common.Interfaces;
using BarBackend.Application.Common.Mappings;
using BarBackend.Application.Common.Models;
using BarBackend.Application.TodoItems.Queries.GetTodoItemsWithPagination;

namespace BarBackend.Application.Ingredients.Queries.GetIngredientsWithPagination;

public record GetIngredientsWithPaginationQuery : IRequest<PaginatedList<IngredientDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetIngredientsWithPaginationQueryHandler : IRequestHandler<GetIngredientsWithPaginationQuery, PaginatedList<IngredientDto>>
{
    private readonly IBarDbContext _context;
    private readonly IMapper _mapper;

    public GetIngredientsWithPaginationQueryHandler(IBarDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<IngredientDto>> Handle(GetIngredientsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Ingredients
            .OrderBy(x => x.Name)
            .ProjectTo<IngredientDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
