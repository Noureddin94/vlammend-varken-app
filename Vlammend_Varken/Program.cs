using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Data;

namespace Vlammend_Varken
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register the DbContext with dependency injection
            builder.Services.AddDbContext<Vlammend_Varken.Core.Data.AppDbConnection>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbConnection>();
            builder.Services.AddRazorPages();

            //builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
            //                .AddIdentityCookies();

            //builder.Services.AddAuthorizationBuilder()
            //                .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

            var app = builder.Build();

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
