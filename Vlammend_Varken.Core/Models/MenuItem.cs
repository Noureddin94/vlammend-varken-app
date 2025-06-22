using System.ComponentModel.DataAnnotations.Schema;

namespace Vlammend_Varken.Models
{
    public class MenuItem : BaseEntity
    {
        public string? Description { get; set; } = null; // Optional description for the menu drinks

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int MenuCategoryId { get; set; }
        public MenuCategory? MenuCategory { get; set; } = null;
        public ICollection<Ingredient> ingredients { get; set; } = new List<Ingredient>();
        public string? Image { get; set; } = null;
        public bool IsActive { get; set; } = true;
    }
}
