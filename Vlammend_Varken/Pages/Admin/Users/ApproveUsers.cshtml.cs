using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.Users
{
    public class ApproveUsersModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public List<User> PendingUsers { get; set; } = new();

        public ApproveUsersModel(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void OnGet()
        {
            PendingUsers = _userManager.Users.Where(u => !u.IsApproved).ToList();
        }

        public async Task<IActionResult> OnPostApproveAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsApproved = true;
                await _userManager.UpdateAsync(user);

                if (!await _roleManager.RoleExistsAsync("Waiter"))
                {
                    await _roleManager.CreateAsync(new IdentityRole<int>("Waiter"));
                }

                // Remove any existing roles first
                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                }

                await _userManager.AddToRoleAsync(user, "Waiter");
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejectAsync(string userId)
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
