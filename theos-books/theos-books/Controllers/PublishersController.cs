using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using theos_books.Business;
using theos_books.Data;
using theos_books.Models;

namespace theos_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private PublisherBO publisherBO;

        public PublishersController(ApplicationDbContext context)
        {
            _context = context;
            publisherBO = new PublisherBO(_context);
        }

        // GET: api/Publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            return await _context.Publishers.ToListAsync();
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisher(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }

        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Publisher>> PutPublisher(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            return await publisherBO.Edit(publisher, getUser());
        }

        // POST: api/Publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
        {
            return await publisherBO.Save(publisher, getUser());
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            await publisherBO.Delete(id, getUser());
            return NoContent();
        }

        private ApplicationUser getUser()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            ApplicationUser user = _context.Users.Find(userId);
            return user;
        }

        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.Id == id);
        }
    }
}
