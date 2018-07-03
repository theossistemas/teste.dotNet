using System.Collections.Generic;
using LivrariaTemp.Models;
using LivrariaTemp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaTemp.Controllers
{
    [Produces("application/json")]
    [Route("api/Livros")]
    public class LivrosController : Controller
    {
        private readonly ILivroRepository _livroRepository;

        public LivrosController(ILivroRepository livroRepo)
        {
            _livroRepository = livroRepo;
        }

        [HttpGet]
        public IEnumerable<Livro> GetAll()
        {
            return _livroRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetLivro")]
        public IActionResult GetById(int id)
        {
            var livro = _livroRepository.Find(id);

            if (livro == null)
                return NotFound();

            return new ObjectResult(livro);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Livro livro)
        {
            if (livro == null)
                return BadRequest();

            _livroRepository.Add(livro);

            return CreatedAtRoute("GetLivro", new { id = livro.LivroId }, livro);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Livro livro)
        {
            if (livro == null || livro.LivroId != id)
                return BadRequest();


            var _livro = _livroRepository.Find(id);
            if (_livro == null)
                return NotFound();

            _livro.Titulo = livro.Titulo;
            _livro.Valor = livro.Valor;
            _livroRepository.Update(_livro);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var livro = _livroRepository.Find(id);

            if (livro == null)
                return NotFound();

            _livroRepository.Remove(id);

            return new NoContentResult();
        }
    }
}