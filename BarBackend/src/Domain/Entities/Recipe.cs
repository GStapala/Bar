using System.ComponentModel.DataAnnotations;

namespace BarBackend.Domain.Entities
{
    public class Recipe : BaseAuditableEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        // Navigation property for the many-to-many relationship
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }
}
