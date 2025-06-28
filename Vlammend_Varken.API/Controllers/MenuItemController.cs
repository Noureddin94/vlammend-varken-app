using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vlammend_Varken.Core.Data;
using Vlammend_Varken.Core.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vlammend_Varken.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly AppDbConnection _context;

        public MenuItemController(AppDbConnection context)
        {
            _context = context;
        }


        // GET: api/<MenuItemController>
        [HttpGet]
        public async Task<ActionResult<List<MenuItem>>> GetMenuItems()
        {
            return Ok(new
            {
                message = "All Menu Items retrieved successfully",
                data = await _context.MenuItems.ToListAsync()
            });
        }

        // GET api/<MenuItemController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItem(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item == null)
            {
                return NotFound(new { message = "Menu Item Not Found" });
            }

            return Ok(new
            {
                message = "Item retrieved successfully",
                data = item
            });
        }

        // POST api/<MenuItemController>
        [HttpPost]
        public async Task<ActionResult<MenuItem>> CreateMenuItem(MenuItem item)
        {
            if (item == null)
            {
                return BadRequest(new { message = "Invalid item data" });
            }
            _context.MenuItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMenuItem), new { id = item.Id }, new
            {
                message = "New Menu Item Created successfully",
                data = item
            });
        }

        // PUT api/<MenuItemController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem(int id, MenuItem item)
        {
            if (id != item.Id)
            {
                return BadRequest(new { message = "Item ID mismatch" });
            }
            var existingItem = await _context.MenuItems.FindAsync(id);
            if (existingItem == null)
            {
                return NotFound(new { message = "Menu Item Not Found" });
            }
            existingItem.Description = item.Description;
            existingItem.Price = item.Price;
            existingItem.MenuCategoryId = item.MenuCategoryId;
            existingItem.IsActive = item.IsActive;
            _context.MenuItems.Update(existingItem);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "Menu Item updated successfully",
                data = existingItem
            });
        }

        // DELETE api/<MenuItemController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item != null)
            {
                _context.MenuItems.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("Menu Item not found");
            }
            return Ok(new
            {
                message = "Menu Item deleted successfully"
            });
        }
    }
}
