using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.Orders
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
        public Order Order { get; set; } = new Order();

        [BindProperty]
        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();

        public List<Table> AvailableTables { get; set; } = new List<Table>();
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        public async Task OnGetAsync()
        {
            AvailableTables = await _context.Tables.ToListAsync();
            MenuItems = await _context.MenuItems.ToListAsync();

            // Initialize with one empty item
            OrderItems.Add(new OrderItemViewModel());
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AvailableTables = await _context.Tables.ToListAsync();
                MenuItems = await _context.MenuItems.ToListAsync();
                return Page();
            }

            // Set default order values
            Order.OrderDate = DateTime.UtcNow;
            Order.Status = OrderStatus.Received;

            // Calculate total amount
            Order.TotalAmount = OrderItems.Sum(oi => oi.Quantity * oi.Price);

            // Save the order
            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            // Save order items
            foreach (var item in OrderItems)
            {
                var orderItem = new OrderOverview
                {
                    OrderId = Order.Id,
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    Note = item.Note,
                    PriceEach = item.Price,
                    PriceTotal = item.Quantity * item.Price
                };
                _context.OrderOverviews.Add(orderItem);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

    public class OrderItemViewModel
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; } = 1;
        public string? Note { get; set; }
        public decimal Price { get; set; }
    }
}

