using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using luizalabs_api_wishlist.Models.Entities;
using luizalabs_api_wishlist.Repositories.Interfaces;
using luizalabs_api_wishlist.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace luizalabs_api_wishlist.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WishesController : ControllerBase
    {
        private readonly IWishRepository _wishRepository;

        public WishesController(IWishRepository wishRepository)
        {
            _wishRepository = wishRepository;
        }


        /// <summary>
        /// Get a list of wishes by user
        /// </summary>
        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<Product>>> Get(long userId, int page_size, int page)
        {
            var products = await _wishRepository.GetWishes(userId, page_size, page);
            if (products.Count() == 0) return NoContent();
            return Ok(products);
        }

        /// <summary>
        /// Add new wish
        /// </summary>
        [HttpPost("{userId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Post(long userId, [FromBody] List<WishProduct> idProduct)
        {
            var wishes = new List<long>();
            foreach (var item in idProduct)
            {
                var wish = await _wishRepository.GetWish(userId, item.idProduct);
                if (wish == null)
                {
                    wishes.Add(await _wishRepository.AddWish(new Wish { userId = userId, productId = item.idProduct }));
                }
            }
            if (wishes.Count  == 0)
            {
                return BadRequest();
            }
            return StatusCode(201, wishes);
        }

        /// <summary>
        /// remove wish
        /// </summary>
        [HttpDelete("{userId}/{productId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(long userId, long productId)
        {
            var wish = await _wishRepository.GetWish(userId, productId);
            if (wish == null)
            {
                return NotFound();
            }
            await _wishRepository.DelWish(userId, productId);
            return Ok();
        }

    }
}
