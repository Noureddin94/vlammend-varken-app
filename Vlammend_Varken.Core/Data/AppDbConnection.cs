using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vlammend_Varken.Models;

namespace Vlammend_Varken.Core.Data
{
    public class AppDbConnection : DbContext
    {
        public AppDbConnection(DbContextOptions<AppDbConnection> options) : base(options)
        {
        }

        // Define your DbSets for the entities here
        public DbSet<MenuCategory> menuCategories { get; set; }
        public DbSet<MenuItem> menuItems { get; set; }
        public DbSet<Ingredient> ingredients { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderOverview> orderOverviews  { get; set; }
        public DbSet<Table> tables { get; set; }
        public DbSet<MergedTable> mergedTables { get; set; }
        public DbSet<User> users { get; set; }


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
