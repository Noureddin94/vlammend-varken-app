using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.Orders
{
    [Authorize(Roles = "Admin, Chef")]
    public class DetailsModel : PageModel
    {
        private readonly AppDbConnection _context;

        public DetailsModel(AppDbConnection context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }
        public Order? Order { get; set; } = new Order
        {
            OrderOverviews = new List<OrderOverview>()
        };
        public List<OrderOverview> OrderItems { get; set; } = new List<OrderOverview>();

        public async Task<IActionResult> OnGetAsync()
        {
            if (OrderId <= 0)
            {
                return NotFound();
            }

            // Load the order with its related data
            Order = await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.OrderOverviews)
                    .ThenInclude(oo => oo.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == OrderId);

            if (Order == null)
            {
                return NotFound();
            }

            // Get the order items separately if needed
            OrderItems = await _context.OrderOverviews
                .Where(oo => oo.OrderId == OrderId)
                .Include(oo => oo.MenuItem)
                .ToListAsync();

            return Page();
        }
    }
}