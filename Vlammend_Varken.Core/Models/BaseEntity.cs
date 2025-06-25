namespace Vlammend_Varken.Core.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
    }
}
