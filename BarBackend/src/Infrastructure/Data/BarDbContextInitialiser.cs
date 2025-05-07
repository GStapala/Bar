using BarBackend.Domain;
using BarBackend.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BarBackend.Infrastructure.Data;

public static class BarDbContextInitialiserExtensions
{
    public static async Task InitialiseBarDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<BarDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class BarDbContextInitialiser
{
    private readonly ILogger<BarDbContextInitialiser> _logger;
    private readonly BarDbContext _context;

    public BarDbContextInitialiser(ILogger<BarDbContextInitialiser> logger, BarDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the Bar database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the Bar database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        // Seed Categories
        if (!_context.Categories.Any())
        {
            var oldRumCategory = new Category { Name = "Old Rum" };
            var whiteRumCategory = new Category { Name = "White Rum" };
            _context.Categories.AddRange(
                new Category { Name = "Rum", SubCategories = new List<Category> { oldRumCategory, whiteRumCategory } },
                new Category { Name = "Vodka" },
                new Category { Name = "Gin" }
            );
        }

        // Seed Ingredients
        if (!_context.Ingredients.Any())
        {
            var rumCategory = _context.Categories.FirstOrDefault(x => x.Name == "Rum");
            var vodkaCategory = _context.Categories.FirstOrDefault(x => x.Name == "Vodka");
            var ginCategory = _context.Categories.FirstOrDefault(x => x.Name == "Gin");
            _context.Ingredients.AddRange(
                new Ingredient { Name = "Rum Anejo", CategoryId = 1, Category = rumCategory! },
                new Ingredient { Name = "Soplica", CategoryId = 2, Category = vodkaCategory! },
                new Ingredient { Name = "Beefeater", CategoryId = 3, Category = ginCategory! }
            );
        }

        // Seed Recipes
        if (!_context.Recipes.Any())
        {
            _context.Recipes.Add(new Recipe
            {
                Name = "Apple Pie",
                Description = "A delicious apple pie recipe.",
                RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient { IngredientId = 1, Quantity = 3, ImportanceLevel = 5 },
                    new RecipeIngredient { IngredientId = 3, Quantity = 1, ImportanceLevel = 4 }
                }
            });
        }

        await _context.SaveChangesAsync();
    }
}
