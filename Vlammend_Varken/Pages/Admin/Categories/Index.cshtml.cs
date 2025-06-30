using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.Categories
{
    [Authorize(Roles = "Admin, Chef")]
    public class IndexModel : PageModel
    {
        private readonly AppDbConnection _context;

        public IndexModel(AppDbConnection context)
        {
            _context = context;
        }

        public IList<MenuCategory> MenuCategories{ get; set; } = default!;

        public async Task OnGetAsync()
        {
            MenuCategories = await _context.MenuCategories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}
