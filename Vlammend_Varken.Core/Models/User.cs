using Microsoft.AspNetCore.Identity;

namespace Vlammend_Varken.Core.Models
{
    public class User : IdentityUser<int>
    {
        public EnumRole Role { get; set; }
    }

    public enum EnumRole
    {
        Admin,
        Chef,
        Waiter,
    }
}
