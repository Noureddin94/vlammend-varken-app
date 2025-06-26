using System.ComponentModel.DataAnnotations.Schema;

namespace Vlammend_Varken.Core.Models
{
    public class Ingredient : BaseEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Quantity { get; set; } = string.Empty; // e.g., "100g", "1 piece", etc.
        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; } = null;
        public bool IsActive { get; set; } = true;
    }
}
