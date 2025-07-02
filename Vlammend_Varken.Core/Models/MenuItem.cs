using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Vlammend_Varken.Core.Models
{
    public class MenuItem : BaseEntity
    {
        public string? Description { get; set; } = null; // Optional description for the menu drinks

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public string? Image { get; set; } = null;
        public bool IsActive { get; set; } = true;
        
        [JsonIgnore]
        public MenuCategory? MenuCategory { get; set; } = null;
        public int MenuCategoryId { get; set; }
    }
}
