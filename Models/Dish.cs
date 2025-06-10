using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vlammend_Varken.Models
{
    public class Dish
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public required List<Ingredient> Ingredients { get; set; }

        public  string? Image { get; set; }

        public required string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public required string Types { get; set; }

        public bool IsActive { get; set; }
    }
}
