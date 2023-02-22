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
    public class TogoOrdersController : ControllerBase
    {
        private readonly RestaurantServiceApiContext _context;

        public TogoOrdersController(RestaurantServiceApiContext context)
        {
            _context = context;
        }

        // GET: api/TogoOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TogoOrder>>> GetTogoOrder()
        {
          if (_context.TogoOrder == null)
          {
              return NotFound();
          }
            return await _context.TogoOrder.ToListAsync();
        }

        // GET: api/TogoOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TogoOrder>> GetTogoOrder(int id)
        {
          if (_context.TogoOrder == null)
          {
              return NotFound();
          }
            var togoOrder = await _context.TogoOrder.FindAsync(id);

            if (togoOrder == null)
            {
                return NotFound();
            }

            return togoOrder;
        }

        // PUT: api/TogoOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTogoOrder(int id, TogoOrder togoOrder)
        {
            if (id != togoOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(togoOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TogoOrderExists(id))
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

        // POST: api/TogoOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TogoOrder>> PostTogoOrder(TogoOrder togoOrder)
        {
          if (_context.TogoOrder == null)
          {
              return Problem("Entity set 'RestaurantServiceApiContext.TogoOrder'  is null.");
          }
            _context.TogoOrder.Add(togoOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTogoOrder", new { id = togoOrder.Id }, togoOrder);
        }

        // DELETE: api/TogoOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTogoOrder(int id)
        {
            if (_context.TogoOrder == null)
            {
                return NotFound();
            }
            var togoOrder = await _context.TogoOrder.FindAsync(id);
            if (togoOrder == null)
            {
                return NotFound();
            }

            _context.TogoOrder.Remove(togoOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TogoOrderExists(int id)
        {
            return (_context.TogoOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
