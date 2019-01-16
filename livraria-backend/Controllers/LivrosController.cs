using System.Linq;
using System.Threading.Tasks;
using livraria_backend.Data;
using livraria_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace livraria_backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class LivrosController : Controller
    {
        private readonly LivrariaContext _context;

        public LivrosController(LivrariaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Livros(){
            return Ok(_context.Livros.OrderBy(l => l.Nome).ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id){
            var header = this.HttpContext.Request.Headers;

            if (!header.ContainsKey("Authorization") || !(header["Authorization"] == "123456")){
                return StatusCode(401);
            }
            return Ok(await _context.Livros.SingleAsync(x=>x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Livro livro){

            var header = this.HttpContext.Request.Headers;

            if (!header.ContainsKey("Authorization") || !(header["Authorization"] == "123456")){
                return StatusCode(401);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();
            return Ok(livro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody]Livro livro){
            
            var header = this.HttpContext.Request.Headers;

            if (!header.ContainsKey("Authorization") || !(header["Authorization"] == "123456")){
                return StatusCode(401);
            }

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
                return BadRequest();
            }

            return Ok(livro);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
             var header = this.HttpContext.Request.Headers;

            if (!header.ContainsKey("Authorization") || !(header["Authorization"] == "123456")){
                return StatusCode(401);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var livro = await _context.Livros.SingleOrDefaultAsync(l => l.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return Ok(livro);
        }
    }
}