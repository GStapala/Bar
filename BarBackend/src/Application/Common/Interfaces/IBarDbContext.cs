using BarBackend.Domain.Entities;

namespace BarBackend.Application.Common.Interfaces;

public interface IBarDbContext
{
    DbSet<Ingredient> Ingredients { get; }
    DbSet<Category> Categories { get;  }
    DbSet<Recipe> Recipes { get;  }
    DbSet<RecipeIngredient> RecipeIngredients { get;  }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
