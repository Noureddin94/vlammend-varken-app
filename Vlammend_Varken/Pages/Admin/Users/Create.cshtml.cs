using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.Users
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public CreateModel(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [BindProperty]
        public User NewUser { get; set; } = new User();
        public List<SelectListItem> AvailableRoles { get; } = Enum.GetValues(typeof(EnumRole))
            .Cast<EnumRole>()
            .Select(r => new SelectListItem
            {
                Value = r.ToString(),
                Text = r.ToString()
            })
            .ToList();
        public async Task<IActionResult> OnGetAsync()
            {
            // Initialize roles if they don't exist
            foreach (var role in Enum.GetNames(typeof(EnumRole)))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }
            return Page();
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    // Ensure PasswordHash is not null before calling CreateAsync
        //    if (string.IsNullOrWhiteSpace(NewUser.PasswordHash))
        //    {
        //        ModelState.AddModelError(nameof(NewUser.PasswordHash), "Password cannot be null or empty.");
        //        return Page();
        //    }

        //    var result = await _userManager.CreateAsync(NewUser, NewUser.PasswordHash);
        //    if (result.Succeeded)
        //    {
        //        // Assign the selected role to the new user
        //        if (!string.IsNullOrEmpty(NewUser.Role) && await _roleManager.RoleExistsAsync(NewUser.Role))
        //        {
        //            await _userManager.AddToRoleAsync(NewUser, NewUser.Role);
        //        }
        //        return RedirectToPage("Index");
        //    }

        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError(string.Empty, error.Description);
        //    }
        //    return Page();
        //}
    }
}
