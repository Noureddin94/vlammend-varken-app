namespace Vlammend_Varken.Models
{
    public class Table
    {
        public int Id { get; set; }

        public int TableNumber { get; set; }

        public int GroupSize { get; set; }

        public string? ReservationName { get; set; }

        public DateTime ReservationDate { get; set; }
    }
}
