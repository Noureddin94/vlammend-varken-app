using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Customer
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public List<MenuCategory> Categories { get; set; } = new();
        public List<MenuItem> MenuItems { get; set; } = new();

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
                var response = await httpClient.GetFromJsonAsync<ApiResponse<List<MenuCategory>>>(
                    "http://localhost:5231/api/MenuCategory"
                );

                if (response?.Data != null)
                {
                    // Filter active categories + active menu items
                    Categories = response.Data
                        .Where(c => c.IsActive)
                        .Select(c => new MenuCategory
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Description = c.Description,
                            Image = c.Image,
                            MenuItems = c.MenuItems
                                .Where(i => i.IsActive)
                                .Select(i => new MenuItem
                                {
                                    Id = i.Id,
                                    Name = i.Name,
                                    Description = i.Description,
                                    Price = i.Price,
                                    Image = i.Image,
                                    Ingredients = i.Ingredients,
                                    IsActive = i.IsActive,
                                    MenuCategoryId = i.MenuCategoryId
                                }).ToList(),
                            IsActive = c.IsActive
                        })
                        .ToList();
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, "Error fetching menu from API");
                Console.WriteLine($"API Error: {ex.Message}");
            }
        }

        public class ApiResponse<T>
        {
            public string? Message { get; set; }
            public T? Data { get; set; }
        }
    }

}
