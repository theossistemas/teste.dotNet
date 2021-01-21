using Bookstore.Data;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BooksController> _logger;
                
        public BooksController(ApplicationDbContext context, ILogger<BooksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var result = await _context.Books.ToListAsync();

            return result.OrderBy(x => x.Name).ToList();
        }




        // POST: Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            if (!(_context.Books.Any(e => e.Name == book.Name && e.Isbn == book.Isbn)))
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
            }
            
            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }


        // PUT: Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            _context.Entry(book).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException er)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(new EventId(), er, null);
                    throw;
                }
            }
            return NoContent();
        }


        // DELETE: Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return book;
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
