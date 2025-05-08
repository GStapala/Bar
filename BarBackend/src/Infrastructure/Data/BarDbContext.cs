using BarBackend.Application.Common.Interfaces;
using BarBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarBackend.Domain
{
    // dotnet ef migrations add InitialBar --project 'src/Infrastructure/Infrastructure.csproj' --startup-project 'src/web' --context 'BarDbContext' --output-dir 'Data/Migrations/Bar'
    // dotnet ef database update --project 'src/Infrastructure/Infrastructure.csproj' --startup-project 'src/web' --context 'BarDbContext'
    public class BarDbContext(DbContextOptions<BarDbContext> options) : DbContext(options), IBarDbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between Recipe and Ingredient
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId);

            // Configure Category-Ingredient relationship
            modelBuilder.Entity<Ingredient>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Ingredients)
                .HasForeignKey(i => i.CategoryId);      
            
            modelBuilder.Entity<Ingredient>()
                .Property(i => i.MeasurementValue)
                .HasPrecision(18, 2); // Adjust precision and scale as needed
        }
    }
}
