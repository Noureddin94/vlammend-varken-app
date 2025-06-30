using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Customer.Categories
{
    public class ShowModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public List<MenuCategory> Categories { get; set; } = new List<MenuCategory>();

        public ShowModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
                var response = await httpClient.GetFromJsonAsync<ApiResponse<List<MenuCategory>>>(
                    "http://localhost:5231/api/MenuCategory");

                if (response?.Data != null)
                {
                    Categories = response.Data.Where(c => c.IsActive).ToList();
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle API errors (log, show error message, etc.)
                ModelState.AddModelError(string.Empty, "Error fetching categories from API");
            }
        }
    }

    // Helper class to match your API response structure
    public class ApiResponse<T>
    {
        public string? Message { get; set; }
        public T? Data { get; set; }
    }

}
    
