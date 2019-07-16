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
    public class UsersController : ControllerBase
    {
        private readonly dbContext _context;

        public UsersController(dbContext context)
        {
            _context = context;

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get(int page_size, int page)
        {
            var skip = (page - 1) * page_size;
            return await _context.Users.Skip(skip).Take(page_size).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]User item)
        {
            _context.Users.Add(item);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

     }
}
