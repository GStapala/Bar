using BarBackend.Application.Common.Interfaces;
using BarBackend.Application.Common.Mappings;
using BarBackend.Application.Common.Models;

namespace BarBackend.Application.Categories.Queries.GetCategoriesWithPagination;

public record GetSubCategoriesWithPaginationQuery : IRequest<PaginatedList<CategoryDto>>
{
    public int ParentCategoryId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetSubCategoriesWithPaginationQueryHandler : IRequestHandler<GetSubCategoriesWithPaginationQuery, PaginatedList<CategoryDto>>
{
    private readonly IBarDbContext _context;
    private readonly IMapper _mapper;

    public GetSubCategoriesWithPaginationQueryHandler(IBarDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CategoryDto>> Handle(GetSubCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .Where(x=>x.ParentCategoryId == request.ParentCategoryId)
            .OrderBy(x => x.Name)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
