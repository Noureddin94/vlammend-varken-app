using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.Classification;

namespace Vlammend_Varken.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int? Quantity { get; set; }
    }
}
