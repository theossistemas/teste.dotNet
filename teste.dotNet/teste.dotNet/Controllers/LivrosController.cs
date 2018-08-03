using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using teste.dotNet.Models;

namespace teste.dotNet.Controllers
{
    [Produces("application/json")]
    [Route("api/Livros")]
    public class LivrosController : Controller
    {
        private readonly Context _context;

        public LivrosController(Context context)
        {
            _context = context;
        }

        // GET: api/Livros
        [HttpGet]
        public IEnumerable<Livro> Get()
        {
            return _context.Livros;
        }

        // GET: api/Livros/5
        [HttpGet("{id}")]
        public async Task<Livro> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                throw new RequestException("Model State is invalid!");
            }

            var livro = await _context.Livros.SingleOrDefaultAsync(m => m.Id == id);

            if (livro == null || livro.Id == 0)
            {
                throw new RequestException("Not Found");
            }

            return livro;
        }

        // PUT: api/Livros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != livro.Id)
            {
                return BadRequest();
            }

            _context.Entry(livro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(id))
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

        // POST: api/Livros
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = livro.Id }, livro);
        }

        // DELETE: api/Livros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var livro = await _context.Livros.SingleOrDefaultAsync(m => m.Id == id);
            if (livro == null || livro.Id == 0)
            {
                return NotFound();
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return Ok(livro);
        }

        private bool LivroExists(int id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }
    }
}