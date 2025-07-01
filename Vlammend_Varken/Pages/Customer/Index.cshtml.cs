using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Customer
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public List<MenuCategory> Categories { get; set; } = new();

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            Console.WriteLine("Fetching categories..."); // Check server logs
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
                var response = await httpClient.GetFromJsonAsync<ApiResponse<List<MenuCategory>>>(
                    "http://localhost:5231/api/MenuCategory");

                Console.WriteLine($"Response received: {response != null}"); // Check server logs

                if (response?.Data != null)
                {
                    Console.WriteLine($"Found {response.Data.Count} categories"); // Check server logs
                    Categories = response.Data.Where(c => c.IsActive).ToList();
                    Console.WriteLine($"{Categories.Count} active categories"); // Check server logs
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}"); // Check server logs
                ModelState.AddModelError(string.Empty, "Error fetching categories from API");
            }
        }

        // Helper class to match your API response structure
        public class ApiResponse<T>
        {
            public string? Message { get; set; }
            public T? Data { get; set; }
        }
    }

}
