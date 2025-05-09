using BarBackend.Domain.Entities;

namespace BarBackend.Application.Categories.Queries.GetCategoriesWithPagination;

public class CategoryDto
{
    public int Id { get; init; }

    public int ParentCategoryId { get; init; }

    public string? Name { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
