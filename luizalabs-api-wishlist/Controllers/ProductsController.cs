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
    public class ProductsController : ControllerBase
    {
        private readonly dbContext _context;

        public ProductsController(dbContext context)
        {
            _context = context;

        }


        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get(int page_size, int page)
        {
            var skip = (page - 1) * page_size;
            return await _context.Products.Skip(skip).Take(page_size).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(long id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product item)
        {
            _context.Products.Add(item);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

     }
}
