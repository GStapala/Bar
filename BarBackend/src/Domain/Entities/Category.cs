using System.ComponentModel.DataAnnotations.Schema;

namespace BarBackend.Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;

        // Navigation property
        public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        
        // Self-referencing relationship for subcategories
        [ForeignKey("ParentCategory")]
        public int? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();
    }
}
