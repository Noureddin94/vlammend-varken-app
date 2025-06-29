using Microsoft.AspNetCore.Identity;

namespace Vlammend_Varken.Core.Models
{
    public class User : IdentityUser<int>
    {
        public EnumRole Role { get; set; } = EnumRole.Waiter; // Default role is Waiter
        public bool IsApproved { get; set; } = false; // Default is not approved
    }

    public enum EnumRole
    {
        Admin,
        Chef,
        Waiter,
    }
}
