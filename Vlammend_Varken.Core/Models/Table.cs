namespace Vlammend_Varken.Core.Models
{
    public class Table
    {
        public int Id { get; set; }

        public int TableNumber { get; set; }
        public TableStatus Status { get; set; } = TableStatus.Available;

        public int GroupSize { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public enum TableStatus
    {
        Available,
        Reserved,
        Occupied,
        Merged
    }
}