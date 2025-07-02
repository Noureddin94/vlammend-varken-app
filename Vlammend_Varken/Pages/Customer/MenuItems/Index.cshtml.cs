using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Customer.MenuItems
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public List<MenuItem> MenuItems { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<ApiResponse<List<MenuItem>>>("http://localhost:5231/api/MenuItem");

            if (response?.Data != null)
            {
                MenuItems = response.Data;
            }
        }

        public class ApiResponse<T>
        {
            public string? Message { get; set; }
            public T? Data { get; set; }
        }
    }
}