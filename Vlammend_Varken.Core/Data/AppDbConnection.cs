using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Core.Data
{
    public class AppDbConnection : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbConnection(DbContextOptions<AppDbConnection> options) : base(options)
        {
        }

        // Define your DbSets for the entities here
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderOverview> OrderOverviews { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<MergedTable> MergedTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MergedTable>()
                .HasOne(mt => mt.MainTable)
                .WithMany()
                .HasForeignKey(mt => mt.MainTableId)
                .OnDelete(DeleteBehavior.Restrict); // or .NoAction

            modelBuilder.Entity<MergedTable>()
                .HasOne(mt => mt.MergedTableReference)
                .WithMany()
                .HasForeignKey(mt => mt.MergedTableId)
                .OnDelete(DeleteBehavior.Restrict); // or .NoAction
            modelBuilder.Entity<MenuCategory>()
                .HasMany(c => c.MenuItems)
                .WithOne(i => i.MenuCategory)
                .HasForeignKey(i => i.MenuCategoryId);

            // Seed data for tables (1-75)
            var tables = new List<Table>();
            for (int i = 1; i <= 75; i++)
            {
                tables.Add(new Table
                {
                    Id = i,
                    TableNumber = i,
                    GroupSize = 2, // Default capacity
                    Status = TableStatus.Available,
                });
            }
            modelBuilder.Entity<Table>().HasData(tables);
        }
    }
}
