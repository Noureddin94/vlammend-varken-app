using Microsoft.AspNetCore.Mvc;

namespace Vlammend_Varken.Models
{
    public class MenuCategory : BaseEntity
    {
        public string? Description { get; set; }
        public string? Image { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public bool IsActive { get; set; } = true;
    }
}
