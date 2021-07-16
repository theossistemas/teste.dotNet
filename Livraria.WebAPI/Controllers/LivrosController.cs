using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Livraria.Domain;
using Livraria.Infra.Data;
using Livraria.Infra.Data.Repositories;

namespace Livraria.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly Context _Context;

        private static LivroRepository LivroRepository;
        public LivrosController(Context context)
        {
            _Context = context;
        }

        // GET: api/Livros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivros()
        {
            var TodosLivros =  await LivroRepository.BuscarTodosLivros();
            return TodosLivros.ToList();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> BuscarLivros(string Nome)
        {
            var NomeLivro = BuscarLivros(Nome);
            return  Ok(NomeLivro);
        }


        // GET: api/Livros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> GetLivro(int id)
        {
            var livro = await _Context.Livros.FindAsync(id);

            if (livro == null)
            {
                return NotFound();
            }

            return livro;
        }

        // PUT: api/Livros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, Livro livro)
        {
            if (id != livro.Id)
            {
                return BadRequest();
            }

            _Context.Entry(livro).State = EntityState.Modified;

            try
            {
                await _Context.SaveChangesAsync();
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(Livro livro)
        {
            _Context.Livros.Add(livro);
            await _Context.SaveChangesAsync();

            return CreatedAtAction("GetLivro", new { id = livro.Id }, livro);
        }

        // DELETE: api/Livros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            var livro = await _Context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            _Context.Livros.Remove(livro);
            await _Context.SaveChangesAsync();

            return NoContent();
        }

        private bool LivroExists(int id)
        {
            return _Context.Livros.Any(e => e.Id == id);
        }
    }
}
