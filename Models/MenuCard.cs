namespace Vlammend_Varken.Models
{
    public class MenuCard : Category
    {
        public List<Dish>? MenuItems { get; set; } = new();
    }
}
