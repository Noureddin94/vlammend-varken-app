using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.MenuItems
{
    [Authorize(Roles = "Admin, Chef")]
    public class CreateModel : PageModel
    {
        private readonly AppDbConnection _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(AppDbConnection context, IWebHostEnvironment environment, ILogger<CreateModel> logger)
        {
            _context = context;
            _environment = environment;
            _logger = logger;
        }

        [BindProperty]
        public MenuItem MenuItem { get; set; } = new();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public IActionResult OnGet()
        {
            ViewData["Categories"] = _context.MenuCategories.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = _context.MenuCategories.ToList();
                return Page();
            }
            if (ImageFile != null && ImageFile.Length > 0)
            {
                try
                {
                    // Generate unique filename to prevent conflicts
                    var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "menuItems");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    // Save relative path
                    MenuItem.Image = $"/uploads/menuItems/{uniqueFileName}";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error uploading menu Item image");
                    ModelState.AddModelError("", "Error uploading image. Please try again.");
                    return Page();
                }
            }

            _context.MenuItems.Add(MenuItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
