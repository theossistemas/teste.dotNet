using Livraria.Data;
using Livraria.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Livraria.Models.PublisherModel;

namespace Livraria.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PublishersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllPublishers")]
        public async Task<ActionResult<PublisherViewModel>> GetAllPublishers()
        {
            return new PublisherViewModel
            {
                AllPublishers = await _context.Publishers.ToListAsync()
            };
        }

        [HttpGet]
        [Route("GetPublisher")]
        public async Task<ActionResult<PublisherViewModel>> GetPublisher(Guid id)
        {
            if (!PublisherExists(id))
                return NotFound();

            return new PublisherViewModel
            {
                SinglePublisher = await _context.Publishers.FindAsync(id)
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPublisher(Guid id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            _context.Entry(publisher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Publisher>> CreatePublisher(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublisher", new { id = publisher.Id }, publisher);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePublisher(Guid id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublisherExists(Guid id)
        {
            return _context.Publishers.Any(e => e.Id == id);
        }
    }
}
