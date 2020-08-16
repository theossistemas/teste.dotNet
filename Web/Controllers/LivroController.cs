using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services.Acesso;
using Services.Livros;
using System;
using System.Collections.Generic;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private ILivroService livroService;

        public LivroController(ILivroService livroService)
        {
            this.livroService = livroService;
        }

        /// <summary>
        /// Retorna todos os livros salvos no sistema
        /// </summary>
        /// <returns>Lista de livros</returns>
        [HttpGet]
        [AllowAnonymous]
        public IList<LivroDTO> FindAll()
        {
            return this.livroService.FindAll();
        }

        /// <summary>
        /// Retorna um livro pelo seu id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna um livro</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public LivroDTO Find(Int64 id)
        {
            return this.livroService.Find(id);
        }

        /// <summary>
        /// Salve um novo livro no banco de dados
        /// </summary>
        /// <param name="livro"></param>
        /// <returns>Retona o livro persistido</returns>
        [HttpPost]
        [Authorize]
        public LivroDTO Save(LivroDTO livro)
        {
            return this.livroService.Save(livro);
        }

        /// <summary>
        /// Atualiza um livro no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="livro"></param>
        /// <returns>Retorna o livro atualizado</returns>
        [HttpPut("{id}")]
        [Authorize]
        public LivroDTO Update(Int64 id, LivroDTO livro)
        {
            livro.Id = id;

            return this.livroService.Save(livro);
        }

        /// <summary>
        /// Retorna uma lista de livros pela pesquisa por título
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns>Retorna uma lista de livros</returns>
        [AllowAnonymous]
        [HttpGet("titulos/{titulo}")]
        public IList<LivroDTO> FindByTitle(String titulo)
        {
            return this.livroService.FindByTitle(titulo);
        }
    }
}
