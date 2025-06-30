using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Customer.Categories
{
    public class ShowModel : PageModel
    {
        public List<MenuCategory> MenuCategories { get; set; } = new();

        public async Task OnGetAsync()
        {
            using var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://localhost:7179/api/MenuCategory");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseContent);
            var dataElement = doc.RootElement.GetProperty("data");

            MenuCategories = JsonSerializer.Deserialize<List<MenuCategory>>(dataElement.GetRawText());
        }
    }
}


