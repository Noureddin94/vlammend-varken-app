using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public IndexModel(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public class UserViewModel
        {
            public string? Id { get; set; }
            public string? Email { get; set; }
            public string Role { get; set; } = "No Role";
            public bool IsApproved { get; set; }
        }

        public List<UserViewModel> Users { get; set; } = new();

        public List<SelectListItem> AvailableRoles { get; } = Enum.GetValues(typeof(EnumRole))
        .Cast<EnumRole>()
        .Select(r => new SelectListItem
        {
            Value = r.ToString(),
            Text = r.ToString()
        })
        .ToList();

        public async Task OnGetAsync()
        {
            // Initialize roles if they don't exist
            foreach (var role in Enum.GetNames(typeof(EnumRole)))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole<int>());
                }
            }

            var allUsers = await _userManager.Users.ToListAsync();

            foreach (var user in allUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);

                // If user is ID = 1 and not already admin, make admin
                if (user.Id == 1 && !roles.Contains("Admin"))
                {
                    if (roles.Any())
                    {
                        await _userManager.RemoveFromRolesAsync(user, roles);
                    }
                    await _userManager.AddToRoleAsync(user, "Admin");
                    user.Role = EnumRole.Admin;
                    user.IsApproved = true;
                    await _userManager.UpdateAsync(user);
                }

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
            if (user == null)
            {
                return NotFound();
            }

            user.IsApproved = !user.IsApproved;

            if (user.IsApproved)
            {
                // When approving, assign Waiter role
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
            else
            {
                // When revoking, remove ALL roles
                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                }
            }

            await _userManager.UpdateAsync(user);
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

        public async Task<IActionResult> OnPostUpdateRoleAsync(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Get current roles
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Remove all existing roles
            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
            }

            // Add new role if it's not "No Role"
            if (!string.IsNullOrEmpty(newRole) && newRole != "No Role")
            {
                await _userManager.AddToRoleAsync(user, newRole);
            }

            return RedirectToPage();
        }
    }
}
