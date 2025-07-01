using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Pages.Admin.MenuItems
{
    [Authorize(Roles = "Admin, Chef")]
    public class EditModel : PageModel
    {
        private readonly AppDbConnection _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<EditModel> _logger;
        public EditModel(AppDbConnection context, IWebHostEnvironment environment, ILogger<EditModel> logger)
        {
            _context = context;
            _environment = environment;
            _logger = logger;
        }

        [BindProperty]
        public MenuItem? MenuItem { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public string? ExistingImagePath { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MenuItem = await _context.MenuItems.FirstOrDefaultAsync(m => m.Id == id);
            if (id == null)
            {
                return NotFound();
            }
            

            ViewData["Categories"] = await _context.MenuCategories.ToListAsync();
            ExistingImagePath = MenuItem?.Image;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToUpdate = await _context.MenuItems.FirstOrDefaultAsync(m => m.Id == id);
            if (itemToUpdate == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = await _context.MenuCategories.ToListAsync();
                ExistingImagePath = itemToUpdate.Image;
                return Page();
            }

            //_context.Attach(MenuItem).State = EntityState.Modified;
            
            // Handle image upload if new file was provided
            if (ImageFile != null && ImageFile.Length > 0)
            {
                try
                {
                    // Delete existing image if it exists
                    if (!string.IsNullOrEmpty(itemToUpdate.Image))
                    {
                        var existingImagePath = Path.Combine(_environment.WebRootPath,
                                                          itemToUpdate.Image.TrimStart('/'));
                        if (System.IO.File.Exists(existingImagePath))
                        {
                            System.IO.File.Delete(existingImagePath);
                        }
                    }
                    // Save new image
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "menuitems");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    
                    // Validate image file type
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();
                    
                    // Check if the file extension is allowed
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("ImageFile", "Invalid image file type. Allowed types: jpg, jpeg, png, gif.");
                        ViewData["Categories"] = await _context.MenuCategories.ToListAsync();
                        ExistingImagePath = itemToUpdate.Image;
                        return Page();
                    }
                    itemToUpdate.Image = $"/uploads/menuitems/{fileName}";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error validating image file type.");
                    ModelState.AddModelError("ImageFile", "An error occurred while processing the image file.");
                    ViewData["Categories"] = await _context.MenuCategories.ToListAsync();
                    ExistingImagePath = itemToUpdate.Image;
                    return Page();
                }                
            }

            // Update the item properties
            if (await TryUpdateModelAsync<MenuItem>(
                itemToUpdate,
                "MenuItem",
                m => m.Name, 
                m => m.Description, 
                m => m.Price, 
                m => m.MenuCategoryId, 
                m => m.Image,
                m => m.IsActive))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(itemToUpdate.Id))
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

            ExistingImagePath = itemToUpdate.Image;
            return Page();
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(e => e.Id == id);
        }
    }
}
