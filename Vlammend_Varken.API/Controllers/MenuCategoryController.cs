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
            return Ok(await _context.menuCategories.ToListAsync());
        }
    }
}