using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using RestaurantService.Api;
using RestaurantService.Api.Data;

namespace RestaurantService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly RestaurantServiceApiContext _context;

        public MenuItemsController(RestaurantServiceApiContext context)
        {
            _context = context;
        }

        // GET: api/MenuItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItem()
        {
          if (_context.MenuItem == null)
          {
              return NotFound();
          }
            return await _context.MenuItem.ToListAsync();
        }

        // GET: api/MenuItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItem(int id)
        {
          if (_context.MenuItem == null)
          {
              return NotFound();
          }
            var menuItem = await _context.MenuItem.FindAsync(id);

            if (menuItem == null)
            {
                return NotFound();
            }

            return menuItem;
        }

        // PUT: api/MenuItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuItem(int id, MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(menuItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemExists(id))
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

        // POST: api/MenuItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuItem>> PostMenuItem(MenuItem menuItem)
        {
          if (_context.MenuItem == null)
          {
              return Problem("Entity set 'RestaurantServiceApiContext.MenuItem'  is null.");
          }
            _context.MenuItem.Add(menuItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuItem", new { id = menuItem.Id }, menuItem);
        }

        [Route("AddMany")]
        [HttpPost()]
        public async Task<ActionResult<List<MenuItem>>> PostMenuItems([FromBody]IEnumerable<MenuItem> menuItems) {
            if (_context.MenuItem == null) {
                return Problem("Entity set 'RestaurantServiceApiContext.MenuItem'  is null.");
            }

            var itemsCreated = new List<MenuItem>();
            foreach(var item in menuItems) {
                itemsCreated.Add(_context.MenuItem.Add(item).Entity);
            }

            await _context.SaveChangesAsync();

            return itemsCreated;
        }

        // DELETE: api/MenuItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            if (_context.MenuItem == null)
            {
                return NotFound();
            }
            var menuItem = await _context.MenuItem.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            _context.MenuItem.Remove(menuItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuItemExists(int id)
        {
            return (_context.MenuItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
