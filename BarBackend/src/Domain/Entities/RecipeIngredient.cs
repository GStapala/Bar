namespace BarBackend.Domain.Entities
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; } = null!;

        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; } = null!;

        public decimal Quantity { get; set; } // Amount of the ingredient in the recipe
        public int ImportanceLevel { get; set; } // 1-5 scale for importance of the ingredient in the recipe
    }
}
