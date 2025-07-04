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
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<EditModel> _logger;

        public DeleteModel(AppDbConnection context, IWebHostEnvironment environment,
                        ILogger<EditModel> logger)
        {
            _context = context;
            _environment = environment;
            _logger = logger;
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

            var menuCategory = await _context.MenuCategories.FindAsync(id);
            if (menuCategory != null)
            {
                _context.MenuCategories.Remove(menuCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
