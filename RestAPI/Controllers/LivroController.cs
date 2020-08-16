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
        public IList<LivroDTO> FindAll()
        {
            return this.livroService.FindAll();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces(applicationJson)]
        public LivroDTO Find(Int64 id)
        {
            return this.livroService.Find(id);
        }

        [HttpPost]
        [Authorize]
        [Produces(applicationJson)]
        [Consumes(applicationJson)]
        public LivroDTO Save(LivroDTO livro)
        {
            return this.livroService.Save(livro);
        }

        [HttpPut("{id}")]
        [Authorize]
        [Produces(applicationJson)]
        [Consumes(applicationJson)]
        public LivroDTO Update(Int64 id, LivroDTO livro)
        {
            livro.Id = id;

            return this.livroService.Save(livro);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(Int64 id)
        {
            this.livroService.Delete(id);
        }

        [AllowAnonymous]
        [HttpGet("titulos/{titulo}")]
        [Produces(applicationJson)]
        public IList<LivroDTO> FindByTitle(String titulo)
        {
            return this.livroService.FindByTitle(titulo);
        }
    }
}
