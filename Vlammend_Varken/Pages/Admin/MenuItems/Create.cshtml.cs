using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.MenuItems
{
    [Authorize(Roles = "Admin, Chef")]
    public class CreateModel : PageModel
    {
        private readonly AppDbConnection _context;

        public CreateModel(AppDbConnection context)
        {
            _context = context;
        }

        [BindProperty]
        public MenuItem MenuItem { get; set; } = new();

        public IActionResult OnGet()
        {
            ViewData["Categories"] = _context.MenuCategories.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = _context.MenuCategories.ToList();
                return Page();
            }

            _context.MenuItems.Add(MenuItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
