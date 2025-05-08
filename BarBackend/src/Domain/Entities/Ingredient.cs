using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarBackend.Domain.Entities
{
    public class Ingredient : BaseAuditableEntity
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public MeasurementType? MeasurementType { get; set; } // ml, g, pcs, etc.
        public decimal MeasurementValue { get; set; }
        
        public string? Description { get; set; }
        
        public int StockQuantity { get; set; }
        
        // Navigation property
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; } // alcohol, mixer, garnish, etc.
        public virtual ICollection<Category> SubCategory { get; set; } = new List<Category>();
        public virtual ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    }
}
