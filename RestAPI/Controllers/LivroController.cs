using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services.Livros;
using System;
using System.Collections.Generic;

namespace RestAPI.Controllers
{
    [Route("api/livro")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private const String applicationJson = "application/json";

        private ILivroService livroService;

        public LivroController(ILivroService livroService)
        {
            this.livroService = livroService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Produces(applicationJson)]
        public IActionResult FindAll()
        {
            IList<LivroDTO> livros = this.livroService.FindAll();

            if (livros == null || livros.Count == 0)
                return NotFound();

            return Ok(livros);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces(applicationJson)]
        public IActionResult Find(Int64 id)
        {
            LivroDTO livro = this.livroService.Find(id);

            if (livro == null)
                return NotFound();

            return Ok(livro);
        }

        [HttpPost]
        [Authorize]
        [Produces(applicationJson)]
        [Consumes(applicationJson)]
        public IActionResult Save(LivroDTO livro)
        {
            IActionResult validacao = this.ValidarLivro(livro);

            if (validacao != null) return validacao;

            livro = this.livroService.Save(livro);

            return CreatedAtAction(nameof(Find), new { id = livro.Id }, livro);
        }

        [HttpPut("{id}")]
        [Authorize]
        [Produces(applicationJson)]
        [Consumes(applicationJson)]
        public IActionResult Update(Int64 id, LivroDTO livro)
        {
            if (this.livroService.Find(id) == null)
                return NotFound();

            IActionResult validacao = this.ValidarLivro(livro);

            if (validacao != null) return validacao;

            livro.Id = id;

            return Accepted(this.livroService.Save(livro));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(Int64 id)
        {
            if (this.livroService.Find(id) == null)
                return NotFound();

            this.livroService.Delete(id);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("titulos/{titulo}")]
        [Produces(applicationJson)]
        public IActionResult FindByTitle(String titulo)
        {
            IList<LivroDTO> livros = this.livroService.FindByTitle(titulo);

            if (livros == null || livros.Count == 0)
                return NotFound();

            return Ok(livros);
        }

        private IActionResult ValidarLivro(LivroDTO livro)
        {
            String validacao = this.livroService.ValidarLivro(livro);

            if (!String.IsNullOrEmpty(validacao))
                return BadRequest(validacao);

            return null;
        }
    }
}
