using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Models;

namespace Vlammend_Varken.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
            public DbSet<Drink> Drinks { get; set; }
            public DbSet<Dish> Dishes { get; set; }
            public DbSet<DailyMenu> DailyMenus { get; set; }
            public DbSet<MenuCard> MenuCards { get; set; }
            public DbSet<DrinksMenu> DrinksMenus { get; set; }
            public DbSet<Ingredient> Ingredients { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderOverview> OrderOverviews { get; set; }
            public DbSet<Reservation> Reservations { get; set; }
            public DbSet<Table> Table { get; set; }
            public DbSet<MergedTables> MergedTables { get; set; }
    }
}
