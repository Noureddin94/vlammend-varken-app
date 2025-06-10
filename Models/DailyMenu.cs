namespace Vlammend_Varken.Models
{
    public class DailyMenu : Category
    {
        public DateTime? Date {  get; set; }
        public List<Dish>? MenuItems { get; set; }
    }
}
