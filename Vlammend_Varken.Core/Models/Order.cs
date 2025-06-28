using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Received;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } = 0.00m;
        // Foreign key to Table
        public int TableId { get; set; }
        public Table? Table { get; set; }
        // Navigation property - list of order lines
        public ICollection<OrderOverview> OrderOverviews { get; set; } = new List<OrderOverview>();
    }

    public enum OrderStatus
    {
        Received,
        InProgress,
        Completed,
        Cancelled
    }
}
