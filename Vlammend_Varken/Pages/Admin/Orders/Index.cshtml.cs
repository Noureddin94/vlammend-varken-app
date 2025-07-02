using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.Orders
{
    [Authorize(Roles = "Admin, Chef")]
    public class IndexModel : PageModel
    {
        private readonly AppDbConnection _context;

        public IndexModel(AppDbConnection context)
        {
            _context = context;
            Orders = new List<Order>();
            AvailableTables = new List<Table>();
            MenuItems = new List<MenuItem>();
            NewOrder = new Order();
        }

        public IList<Order> Orders { get; set; }
        public IList<Table> AvailableTables { get; set; }
        public IList<MenuItem> MenuItems { get; set; }

        [BindProperty]
        public Order NewOrder { get; set; }

        [BindProperty]
        public List<OrderOverview> OrderItems { get; set; } = new List<OrderOverview>();

        public string GetStatusBadgeClass(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Received => "btn-primary text-white",
                OrderStatus.InProgress => "btn-warning text-dark",
                OrderStatus.Completed => "btn-success text-white",
                OrderStatus.Cancelled => "btn-danger text-white",
                _ => "btn-secondary text-white"
            };
        }
        public string GetStatusDisplayText(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Received => "New Order",
                OrderStatus.InProgress => "Preparing",
                OrderStatus.Completed => "Ready",
                OrderStatus.Cancelled => "Cancelled",
                _ => status.ToString()
            };
        }

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.OrderOverviews)
                .ThenInclude(oo => oo.MenuItem)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            AvailableTables = await _context.Tables.ToListAsync();
            MenuItems = await _context.MenuItems.ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NewOrder.TotalAmount = OrderItems.Sum(oi => oi.Quantity * oi.PriceEach);
            NewOrder.OrderDate = DateTime.Now;

            _context.Orders.Add(NewOrder);
            await _context.SaveChangesAsync();

            // Add order items
            foreach (var item in OrderItems)
            {
                item.OrderId = NewOrder.Id;
                item.PriceTotal = item.Quantity * item.PriceEach;
                _context.OrderOverviews.Add(item);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetOrderItemsAsync(int id)
        {
            var orderItems = await _context.OrderOverviews
                .Where(oo => oo.OrderId == id)
                .Include(oo => oo.MenuItem)
                .ToListAsync();

            return Partial("_OrderItemsPartial", orderItems);
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = status;
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
