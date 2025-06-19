using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vlammend_Varken.Models
{
    public class OrderOverview
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? Note { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceEach { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceTotal { get; set; }

        // Navigation property
        public int DishId { get; set; }
        public Dish Dish { get; set; } = default!;
    }
}
