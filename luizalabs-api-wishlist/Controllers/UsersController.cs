using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using luizalabs_api_wishlist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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


        /// <summary>
        /// Get a list of users
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<User>>> Get(int page_size, int page)
        {
            var skip = (page - 1) * page_size;
            return await _context.Users.Skip(skip).Take(page_size).ToListAsync();
        }

        /// <summary>
        /// Get user by id
        /// </summary>        
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<User>> Get(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        /// <summary>
        /// Add new user
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Post([FromBody]User item)
        {
            _context.Users.Add(item);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

     }
}
