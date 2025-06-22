using System.ComponentModel.DataAnnotations.Schema;

namespace Vlammend_Varken.Models
{
    public class Ingredient : BaseEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; } = null;
        public bool IsActive { get; set; } = true;
    }
}
