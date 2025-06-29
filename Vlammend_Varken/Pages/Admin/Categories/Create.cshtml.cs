using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.Categories
{
    [Authorize(Roles = "Admin, Chef")]
    public class CreateModel : PageModel
    {
        private readonly AppDbConnection _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(AppDbConnection context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public MenuCategory MenuCategory { get; set; } = new();
        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ImageFile != null)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // Save only relative path for use in src="/uploads/..."
                MenuCategory.Image = "/uploads/" + fileName;
            }

            _context.MenuCategories.Add(MenuCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
