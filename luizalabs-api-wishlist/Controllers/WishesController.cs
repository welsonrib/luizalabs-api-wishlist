using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using luizalabs_api_wishlist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace luizalabs_api_wishlist.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WishesController : ControllerBase
    {
        private readonly dbContext _context;

        public WishesController(dbContext context)
        {
            _context = context;

        }


        /// <summary>
        /// Get a list of wishes by user
        /// </summary>
        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<Product>>> Get(long userId, int page_size, int page)
        {
            var skip = (page - 1) * page_size;
            return await _context.Wishes.Where(c => c.userId == c.userId).Skip(skip).Take(page_size).Select (c => c.product).ToListAsync();
        }

        /// <summary>
        /// Add new wish
        /// </summary>
        [HttpPost("{userId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Post(long userId, [FromBody] List<Product> idProduct)
        {
            var wishes = new List<Wish>();
            foreach (var item in idProduct)
            {
                wishes.Add(new Wish { userId = userId, productId = item.id });
            }
            await _context.Wishes.AddRangeAsync(wishes.AsEnumerable());
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        /// <summary>
        /// remove wish
        /// </summary>
        [HttpDelete("{userId}/{productId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(long userId, long productId)
        {
            var wish = await _context.Wishes.FirstOrDefaultAsync(c => c.userId ==userId && c.productId == productId);
            if (wish == null)
            {
                return NotFound();
            }
            _context.Wishes.Remove(wish);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
