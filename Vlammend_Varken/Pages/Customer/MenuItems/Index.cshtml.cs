using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Vlammend_Varken.Pages.Customer.MenuItems
{
    [Authorize(Roles = "Customer")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
