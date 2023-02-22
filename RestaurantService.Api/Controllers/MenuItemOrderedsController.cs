using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantService.Api;
using RestaurantService.Api.Data;

namespace RestaurantService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemOrderedsController : ControllerBase
    {
        private readonly RestaurantServiceApiContext _context;

        public MenuItemOrderedsController(RestaurantServiceApiContext context)
        {
            _context = context;
        }

        // GET: api/MenuItemOrdereds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemOrdered>>> GetMenuItemOrdered()
        {
          if (_context.MenuItemOrdered == null)
          {
              return NotFound();
          }
            return await _context.MenuItemOrdered.ToListAsync();
        }

        // GET: api/MenuItemOrdereds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemOrdered>> GetMenuItemOrdered(int id)
        {
          if (_context.MenuItemOrdered == null)
          {
              return NotFound();
          }
            var menuItemOrdered = await _context.MenuItemOrdered.FindAsync(id);

            if (menuItemOrdered == null)
            {
                return NotFound();
            }

            return menuItemOrdered;
        }

        // PUT: api/MenuItemOrdereds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuItemOrdered(int id, MenuItemOrdered menuItemOrdered)
        {
            if (id != menuItemOrdered.Id)
            {
                return BadRequest();
            }

            _context.Entry(menuItemOrdered).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemOrderedExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MenuItemOrdereds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuItemOrdered>> PostMenuItemOrdered(MenuItemOrdered menuItemOrdered)
        {
          if (_context.MenuItemOrdered == null)
          {
              return Problem("Entity set 'RestaurantServiceApiContext.MenuItemOrdered'  is null.");
          }
            _context.MenuItemOrdered.Add(menuItemOrdered);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuItemOrdered", new { id = menuItemOrdered.Id }, menuItemOrdered);
        }

        // DELETE: api/MenuItemOrdereds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItemOrdered(int id)
        {
            if (_context.MenuItemOrdered == null)
            {
                return NotFound();
            }
            var menuItemOrdered = await _context.MenuItemOrdered.FindAsync(id);
            if (menuItemOrdered == null)
            {
                return NotFound();
            }

            _context.MenuItemOrdered.Remove(menuItemOrdered);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuItemOrderedExists(int id)
        {
            return (_context.MenuItemOrdered?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
