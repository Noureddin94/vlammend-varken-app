using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.MenuItems
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbConnection _context;

        public DeleteModel(AppDbConnection context)
        {
            _context = context;
        }

        [BindProperty]
        public MenuItem? MenuItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MenuItem = await _context.MenuItems
                .Include(m => m.MenuCategory)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (MenuItem == null)
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

            var menuitem = await _context.MenuItems.FindAsync(id);
            if (menuitem != null)
            {
                MenuItem = menuitem;
                _context.MenuItems.Remove(MenuItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
