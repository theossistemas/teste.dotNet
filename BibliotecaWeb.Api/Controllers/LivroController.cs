using LivrariaWeb.Dto;
using LivrariaWeb.Service;
using LivrariaWeb.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;
        private readonly ILogger<LivroController> _logger;


        public LivroController(ILivroService livroService, ILogger<LivroController> logger)
        {
            _livroService = livroService;
            _logger = logger;

        }


        // GET: api/<LivrosController>
        /// <summary>
        /// Obter todos os Livros
        /// </summary>
        /// <remarks>
        /// 
        /// 
        ///     Obtém todos os Livros cadastrados.
        ///     
        /// </remarks>
        [HttpGet]
        [Route("GetAllLivros")]


        public async Task<IEnumerable<DtoLivro>> GetAllLivros()
        {            
            return await _livroService.GetAll();
        }

        // POST api/<LivroController>
        /// <summary>
        /// Cadastrar Livro - Requer Autenticação
        /// </summary>
        /// <remarks>
        /// 
        ///  Cadastra livros de acordo com as seguintes regras
        ///     Não é permitido a inclusão de Livros com nome já existente na base de dados.
        ///     Não é permitido a inclusão de Livros com nome do autor já existente na base de dados.
        /// </remarks>
        [Authorize]
        [HttpPost]
        [Route("Cadastrar")]
        public async Task<ActionResult> Cadastrar([FromBody] DtoLivro dtoLivroRequest)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid) return BadRequest(allErrors);
            
            var dtoLivro = await _livroService.Cadastrar(dtoLivroRequest);
            return Ok(dtoLivro);

        }

        // GET api/<LivrosController>/
        /// <summary>
        /// Obter Livro por id
        /// </summary>
        /// <remarks>
        /// 
        /// 
        ///     Obtêm o Livro por id. Caso você não tenha o id consulte o endpoint GetAllLivros
        ///     
        /// </remarks>
        [HttpGet]
        [Route("GetByIdLivro")]
        public async Task<ActionResult> GetByIdLivro([FromQuery] int id)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid) return BadRequest(allErrors);
            var dtoLivro = await _livroService.GetLivroById(id);
            return Ok(dtoLivro);
        }

        /// <summary>
        /// Editar um Livro - Requer Autenticação
        /// </summary>
        /// <remarks>
        /// Edita um Livro já cadastrado
        /// 
        ///     A edição do livro segue a regra do endpoint Cadastrar.
        ///     
        /// </remarks>
        // PUT api/<LivrosController>/5
        [Authorize]
        [HttpPut]
        [Route("EditarLivro")]
        public async Task<ActionResult> EditarLivro([FromBody] DtoLivro dtoLivro)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid) return BadRequest(allErrors);


            if (dtoLivro == null) return BadRequest(new { Mensagem = "Objeto nulo" });
            var update = _livroService.UpdateLivro(dtoLivro).Result;

            return Ok(update);
        }

        // DELETE api/<LivroController>/5
        /// <summary>
        /// Deletar um Livro - Requer Autenticação
        /// </summary>
        /// <remarks>
        /// Para deletar um livro passe o Id como parâmetro. Caso você não tenha o id consulte o endpoint GetAllLivros
        /// 
        ///     Assim que o endpoint DeleteLivro for executado essa operação não terá como ser desfeita.
        ///     
        /// </remarks>
        [Authorize]
        [HttpDelete]
        [Route("DeleteLivro")]
        public async Task<ActionResult> DeleteCurso([FromQuery] int id)
        {
            if (id == 0) return BadRequest(new { Mensagem = "id deve ser diferente de zero." });

            var delete = await _livroService.DeleteCurso(id);

            return Ok(delete);

        }
    }
}
