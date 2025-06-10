using Microsoft.AspNetCore.Identity;

namespace Vlammend_Varken.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required string Phone { get; set; }

        public Role Role { get; set; } 
    }
}
