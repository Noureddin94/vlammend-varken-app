using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Core.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbConnection context,
                                           UserManager<User> userManager,
                                           RoleManager<IdentityRole<int>> roleManager)
        {

            // Ensure database exists and tables are up to date
            await context.Database.MigrateAsync();

            // Seed roles
            foreach (var role in Enum.GetNames(typeof(EnumRole)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }

            // Seed users
            if (await userManager.FindByEmailAsync("Admin@gmail.com") == null)
            {
                var admin = new User
                {
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    EmailConfirmed = true,
                    Role = EnumRole.Admin,
                    IsApproved = true
                };
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "waiter1"))
            {
                var waiter1 = new User { UserName = "waiter1", Email = "waiter1@mail.com", Role = EnumRole.Waiter, IsApproved = true };
                await userManager.CreateAsync(waiter1, "Waiter@123");
                await userManager.AddToRoleAsync(waiter1, "Waiter");

                var waiter2 = new User { UserName = "waiter2", Email = "waiter2@mail.com", Role = EnumRole.Waiter, IsApproved = false };
                await userManager.CreateAsync(waiter2, "Waiter@123");
                await userManager.AddToRoleAsync(waiter2, "Waiter");
            }

            // Seed categories
            if (!context.MenuCategories.Any())
            {
                var cat1 = new MenuCategory { Name = "Dagfavorieten", Description = "Special daily favorites" };
                var cat2 = new MenuCategory { Name = "Vaste Gerechten", Description = "Fixed dishes" };
                var cat3 = new MenuCategory { Name = "Dranken", Description = "Drinks" };
                context.MenuCategories.AddRange(cat1, cat2, cat3);
                await context.SaveChangesAsync();

                // Seed items
                context.MenuItems.AddRange(
                    new MenuItem { Name = "Spaghetti Bolognese", MenuCategory = cat1, Price = 12.5m },
                    new MenuItem { Name = "Kip Sate", MenuCategory = cat1, Price = 13.0m },
                    new MenuItem { Name = "Zalm met Groenten", MenuCategory = cat1, Price = 15.5m },
                    new MenuItem { Name = "Dagschotel Surprise", MenuCategory = cat1, Price = 11.0m },

                    new MenuItem { Name = "Ribeye Steak", MenuCategory = cat2, Price = 20m },
                    new MenuItem { Name = "Vegetarische Lasagne", MenuCategory = cat2, Price = 10m },
                    new MenuItem { Name = "Hamburger Deluxe", MenuCategory = cat2, Price = 9.5m },
                    new MenuItem { Name = "Schnitzel", MenuCategory = cat2, Price = 14m },

                    new MenuItem { Name = "Coca-Cola", MenuCategory = cat3, Price = 2.5m },
                    new MenuItem { Name = "Heineken Bier", MenuCategory = cat3, Price = 3.0m },
                    new MenuItem { Name = "Espresso", MenuCategory = cat3, Price = 2m },
                    new MenuItem { Name = "Witte Wijn", MenuCategory = cat3, Price = 4m }
                );
                await context.SaveChangesAsync();
            }

            // Seed tables
            if (!context.Tables.Any())
            {
                var tables = Enumerable.Range(1, 75).Select(i => new Table { TableNumber = i, GroupSize = (i % 6) + 2 }).ToList();
                context.Tables.AddRange(tables);
                await context.SaveChangesAsync();

                // Merge example
                context.MergedTables.AddRange(
                    new MergedTable { MainTableId = tables[0].Id, MergedTableId = tables[1].Id, MergedBy = "admin" },
                    new MergedTable { MainTableId = tables[2].Id, MergedTableId = tables[3].Id, MergedBy = "admin" }
                );
                await context.SaveChangesAsync();
            }

            // Seed orders
            if (!context.Orders.Any())
            {
                var random = new Random();
                var tables = context.Tables.Take(5).ToList();
                var items = context.MenuItems.ToList();

                foreach (var table in tables)
                {
                    var order = new Order
                    {
                        TableId = table.Id,
                        Status = (OrderStatus)random.Next(0, 4),
                        TotalAmount = 0,
                        OrderDate = DateTime.Now.AddMinutes(-random.Next(0, 1000))
                    };
                    context.Orders.Add(order);
                    await context.SaveChangesAsync();

                    // Add order items
                    var orderItems = new List<OrderOverview>();
                    for (int i = 0; i < 3; i++)
                    {
                        var menuItem = items[random.Next(items.Count)];
                        var qty = random.Next(1, 4);
                        orderItems.Add(new OrderOverview
                        {
                            OrderId = order.Id,
                            MenuItemId = menuItem.Id,
                            Quantity = qty,
                            PriceEach = menuItem.Price,
                            PriceTotal = menuItem.Price * qty
                        });
                    }
                    order.TotalAmount = orderItems.Sum(oi => oi.PriceTotal);
                    context.OrderOverviews.AddRange(orderItems);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
 }
