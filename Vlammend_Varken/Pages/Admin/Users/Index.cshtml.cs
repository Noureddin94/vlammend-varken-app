using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public IndexModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public class UserViewModel
        {
            public string? Id { get; set; }
            public string? Email { get; set; }
            public string? Role { get; set; }
            public bool IsApproved { get; set; }
        }

        public List<UserViewModel> Users { get; set; } = new();

        public async Task OnGetAsync()
        {
            var allUsers = await _userManager.Users.ToListAsync();

            foreach (var user in allUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);

                Users.Add(new UserViewModel
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "No Role",
                    IsApproved = user.IsApproved,
                });
            }
        }

        public async Task<IActionResult> OnPostToggleApprovalAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsApproved = !user.IsApproved;
                if (user.IsApproved)
                {
                    user.IsApproved = true;
                }
                else
                {
                    user.IsApproved = false;
                    // Optionally, remove the user from roles if not approved
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Any())
                    {
                        await _userManager.RemoveFromRolesAsync(user, roles);
                    }
                }
                await _userManager.UpdateAsync(user);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToPage();
        }
    }
}
