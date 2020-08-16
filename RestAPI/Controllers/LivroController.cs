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
            return Ok(this.livroService.FindAll());
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

            livro.Id = id;

            return Accepted(this.livroService.Save(livro));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(Int64 id)
        {
            this.livroService.Delete(id);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("titulos/{titulo}")]
        [Produces(applicationJson)]
        public IActionResult FindByTitle(String titulo)
        {
            return Ok(this.livroService.FindByTitle(titulo));
        }
    }
}
