using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.MenuItems
{
    public class IndexModel : PageModel
    {
        private readonly AppDbConnection _context;

        public IndexModel(AppDbConnection context)
        {
            _context = context;
        }

        public IList<MenuItem> MenuItems { get; set; } = default!;

        public async Task OnGetAsync()
        {
            MenuItems = await _context.MenuItems
                .Include(m => m.MenuCategory)
                .ToListAsync();
        }
    }
}
