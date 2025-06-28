using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vlammend_Varken.Core.Models
{
    public class OrderOverview
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        
        public int Quantity { get; set; }

        public string? Note { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceEach { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceTotal { get; set; }
    }
}
