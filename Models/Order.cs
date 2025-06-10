using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vlammend_Varken.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Foreign key to Table
        public int TableId { get; set; }
        public Table Table { get; set; } = default!;

        // Navigation property - list of order lines
        public List<OrderOverview> OrderOverviews { get; set; } = new();
    }
}
