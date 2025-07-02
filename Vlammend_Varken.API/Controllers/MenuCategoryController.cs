using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vlammend_Varken.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuCategoryController : ControllerBase
    {
        private readonly AppDbConnection _context;

        public MenuCategoryController(AppDbConnection context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MenuCategory>>> GetMenuCategories()
        {
            var categories = await _context.MenuCategories
            .Include(c => c.MenuItems) // Include menu items
            .ThenInclude(i => i.Ingredients) 
            .Where(c => c.IsActive)    // Only active categories
            .ToListAsync();
            return Ok(new
            {
                message = "All categories retrieved successfully",
                data = await _context.MenuCategories.ToListAsync()
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuCategory>> GetMenuCategory(int id)
        {
            var category = await _context.MenuCategories
            .Include(c => c.MenuItems)
            .ThenInclude(i => i.Ingredients)
            .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }
            return Ok(new
            {
                message = "Category retrieved successfully",
                data = category
            });
        }

        [HttpPost]
        public async Task<ActionResult<MenuCategory>> CreateMenuCategory(MenuCategory category)
        {
            if (category == null)
            {
                return BadRequest(new { message = "Invalid category data" });
            }
            _context.MenuCategories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMenuCategory), new { id = category.Id }, new
            {
                message = "Category created successfully",
                data = category
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuCategory(int id, MenuCategory category)
        {
            if (id != category.Id)
            {
                return BadRequest(new { message = "Category ID mismatch" });
            }
            var existingCategory = await _context.MenuCategories.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound(new { message = "Category not found" });
            }
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.IsActive = category.IsActive;
            _context.MenuCategories.Update(existingCategory);
            try
            {
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return Ok(new { message = "Updated", data = existingCategory });
                }
                return BadRequest(new { message = "Nothing was updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal error", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuCategory(int id)
        {
            var category = await _context.MenuCategories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }
            _context.MenuCategories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Category deleted successfully" });
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<MenuCategory>>> GetActiveMenuCategories()
        {
            var activeCategories = await _context.MenuCategories
                .Where(c => c.IsActive)
                .ToListAsync();
            return Ok(new
            {
                message = "Active categories retrieved successfully",
                data = activeCategories
            });
        }

    }
}