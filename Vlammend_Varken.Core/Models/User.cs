namespace Vlammend_Varken.Core.Models
{
    public class User : BaseEntity
    {
        public required string Email { get; set; } = string.Empty;

        public required string PasswordHash { get; set; }

        public required string Phone { get; set; }

        public EnumRole Role { get; set; }
    }

    public enum EnumRole
    {
        Admin,
        Manager,
        Staff,
        Chef,
        Waiter,
        Supplier
    }
}
