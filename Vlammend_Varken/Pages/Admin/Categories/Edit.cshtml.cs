using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;
using System.Security.Cryptography;

namespace Vlammend_Varken.Pages.Admin.Categories
{
    [Authorize(Roles = "Admin, Chef")]
    public class EditModel : PageModel
    {
        private readonly AppDbConnection _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<EditModel> _logger;

        public EditModel(AppDbConnection context,
                        IWebHostEnvironment environment,
                        ILogger<EditModel> logger)
        {
            _context = context;
            _environment = environment;
            _logger = logger;
        }

        [BindProperty]
        public MenuCategory? MenuCategory { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public string? ExistingImagePath { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MenuCategory = await _context.MenuCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (MenuCategory == null)
            {
                return NotFound();
            }

            ExistingImagePath = MenuCategory.Image;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryToUpdate = await _context.MenuCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (categoryToUpdate == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ExistingImagePath = categoryToUpdate.Image;
                return Page();
            }

            // Handle image upload if new file was provided
            if (ImageFile != null && ImageFile.Length > 0)
            {
                try
                {
                    // Delete old image if it exists
                    if (!string.IsNullOrEmpty(categoryToUpdate.Image))
                    {
                        var oldImagePath = Path.Combine(_environment.WebRootPath,
                                                     categoryToUpdate.Image.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Save new image
                    var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "categories");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    categoryToUpdate.Image = $"/uploads/categories/{uniqueFileName}";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating category image");
                    ModelState.AddModelError("", "Error updating image. Please try again.");
                    ExistingImagePath = categoryToUpdate.Image;
                    return Page();
                }
            }

            // Update other properties
            if (await TryUpdateModelAsync<MenuCategory>(
                categoryToUpdate,
                "MenuCategory",
                c => c.Name,
                c => c.Description,
                c => c.IsActive))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuCategoryExists(categoryToUpdate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToPage("./Index");
            }

            ExistingImagePath = categoryToUpdate.Image;
            return Page();
        }

        private bool MenuCategoryExists(int id)
        {
            return _context.MenuCategories.Any(e => e.Id == id);
        }
    }
}