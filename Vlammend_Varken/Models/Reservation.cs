namespace Vlammend_Varken.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public string? ReservationName { get; set; }

        public DateTime ReservationDate { get; set; }

        public int GroupSize { get; set; }

        public int TableNumber { get; set; }
    }
}
