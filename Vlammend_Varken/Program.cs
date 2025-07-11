using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register the DbContext with dependency injection
            builder.Services.AddDbContext<Vlammend_Varken.Core.Data.AppDbConnection>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbConnection>();

            // Add services to the container.
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Add Identity services for user management
            builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                // Configure Identity options
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppDbConnection>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

            builder.Services.AddRazorPages();

            // Add MVC controllers
            builder.Services.AddHttpClient();

            // Add authorization policies
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("ChefOnly", policy => policy.RequireRole("Chef"));
                options.AddPolicy("WaiterOnly", policy => policy.RequireRole("Waiter"));
            });
            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Ensure the database is created and seeded
            if (args.Contains("--seed")) // to use this, run the app with "dotnet run -- seed"
            {
                Console.WriteLine("Seeding database...");
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<AppDbConnection>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

                    // Apply migrations and seed the database
                    await DbSeeder.SeedAsync(context, userManager, roleManager);
                }
                Console.WriteLine("Database seeded successfully.");
                return; // exit after seeding
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Redirect root to customer page
            app.MapGet("/", () => Results.Redirect("/Customer/Index"));

            app.Run();
        }
    }
}
