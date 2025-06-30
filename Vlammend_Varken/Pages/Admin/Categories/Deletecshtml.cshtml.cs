using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.Categories
{
    [Authorize(Roles = "Admin, Chef")]
    public class DeleteModel : PageModel
    {
        private readonly AppDbConnection _context;

        public DeleteModel(AppDbConnection context)
        {
            _context = context;
        }

        [BindProperty]
        public MenuCategory? MenuCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MenuCategory = await _context.MenuCategories
                .Include(m => m.MenuItems)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (MenuCategory == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCategory = await _context.MenuCategories.FindAsync(MenuCategory);
            if (menuCategory != null)
            {
                MenuCategory = menuCategory;
                _context.MenuCategories.Remove(MenuCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
