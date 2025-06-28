using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vlammend_Varken.Core.Data;

namespace Vlammend_Varken.Pages.Admin
{
    [Authorize(Roles = "Admin, Chef")]
    public class IndexModel : PageModel
    {
        private readonly AppDbConnection _context;

        public IndexModel(AppDbConnection context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
    }
}
