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

        await initialiser.ClearAllDataAsync();
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
        //subcategories
        var oldRumCategory = new Category { Name = "Old Rum" };
        var whiteRumCategory = new Category { Name = "White Rum" };
        
        var syrupCategory = new Category { Name = "Syrups" };
        var fruitsCategory = new Category { Name = "Fruits" };
        var otherCategory = new Category { Name = "Other" };
        
        
        var rumCategory = new Category { Name = "Rum", SubCategories = new List<Category> { oldRumCategory, whiteRumCategory } };
        var vodkaCategory = new Category { Name = "Vodka" };
        var ginCategory = new Category { Name = "Gin" };
        var ingredientsCategory = new Category { Name = "Ingredients", SubCategories = new List<Category> { otherCategory, syrupCategory, fruitsCategory } };
        
        // Seed Categories
        if (!_context.Categories.Any())
        {
            _context.Categories.AddRange(
                rumCategory,
                vodkaCategory,
                ginCategory,
                ingredientsCategory
            );
            await _context.SaveChangesAsync();
        }
        

        var rumAnejo = new Ingredient { Name = "Rum Anejo", CategoryId = 1, Category = rumCategory!, SubCategory = new List<Category>() { oldRumCategory } };
        var whiteRum = new Ingredient { Name = "White rum", CategoryId = 1, Category = rumCategory!, SubCategory = new List<Category>() { whiteRumCategory } };
        var soplica = new Ingredient { Name = "Soplica", CategoryId = 2, Category = vodkaCategory! };
        var beefeater = new Ingredient { Name = "Beefeater", CategoryId = 3, Category = ginCategory! };
        var grenadine = new Ingredient { Name = "Grenadine", CategoryId = 4, Category = _context.Categories.FirstOrDefault(x => x.Name == "Syrups")! };
        var lemon = new Ingredient { Name = "Lemon", CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Fruits")!.Id, Category = _context.Categories.FirstOrDefault(x => x.Name == "Fruits")! };
        var mint = new Ingredient { Name = "Mint", CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Fruits")!.Id, Category = _context.Categories.FirstOrDefault(x => x.Name == "Fruits")! };
        var sugar = new Ingredient { Name = "Sugar", CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Other")!.Id, Category = _context.Categories.FirstOrDefault(x => x.Name == "Other")! };
        var limeSyrup = new Ingredient { Name = "Lime Syrup", CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Syrups")!.Id, Category = _context.Categories.FirstOrDefault(x => x.Name == "Syrups")! };
        var pineapple = new Ingredient { Name = "Pineapple", CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Fruits")!.Id, Category = _context.Categories.FirstOrDefault(x => x.Name == "Fruits")! };
        var orange = new Ingredient { Name = "Orange", CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Fruits")!.Id, Category = _context.Categories.FirstOrDefault(x => x.Name == "Fruits")! };
        var coconut = new Ingredient { Name = "Coconut", CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Fruits")!.Id, Category = _context.Categories.FirstOrDefault(x => x.Name == "Fruits")! };
        var mintSyrup = new Ingredient { Name = "Mint Syrup", CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Syrups")!.Id, Category = _context.Categories.FirstOrDefault(x => x.Name == "Syrups")! };
        var lemonSyrup = new Ingredient { Name = "Lemon Syrup", CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Syrups")!.Id, Category = _context.Categories.FirstOrDefault(x => x.Name == "Syrups")! };

        // Seed Ingredients
        if (!_context.Ingredients.Any())
        {
            _context.Ingredients.AddRange(
                rumAnejo,
                whiteRum,
                soplica,
                beefeater,
                grenadine,
                lemon,
                mint,
                sugar,
                limeSyrup,
                pineapple,
                orange,
                coconut,
                mintSyrup,
                lemonSyrup
            );
            await _context.SaveChangesAsync();
        }

        // Seed Recipes
        if (!_context.Recipes.Any())
        {
            _context.Recipes.Add(new Recipe
            {
                Name = "Mai tai # mop",
                Description = "Super mai tai test",
                RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient { IngredientId = rumAnejo.Id, Quantity = 60, ImportanceLevel = 5 },
                    new RecipeIngredient { IngredientId = whiteRum.Id, Quantity = 60, ImportanceLevel = 5 },
                    new RecipeIngredient { IngredientId = limeSyrup.Id, Quantity = 60, ImportanceLevel = 5 },
                }
            });
        }

        await _context.SaveChangesAsync();
    }

    public async Task ClearAllDataAsync()
    {
        try
        {
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE RecipeIngredients NOCHECK CONSTRAINT ALL;");
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Recipes NOCHECK CONSTRAINT ALL;");
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Ingredients NOCHECK CONSTRAINT ALL;");
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Categories NOCHECK CONSTRAINT ALL;");
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE MeasurementType NOCHECK CONSTRAINT ALL;");

            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Recipes;");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM RecipeIngredients;");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Ingredients;");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Categories;");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM MeasurementType;");
            

            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE RecipeIngredients CHECK CONSTRAINT ALL;");
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Recipes CHECK CONSTRAINT ALL;");
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Ingredients CHECK CONSTRAINT ALL;");
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Categories CHECK CONSTRAINT ALL;");
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE MeasurementType CHECK CONSTRAINT ALL;");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while clearing the Bar database.");
            throw;
        }
    }
}
