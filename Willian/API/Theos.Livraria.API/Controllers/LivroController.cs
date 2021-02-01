using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Theos.Livraria.Domain.Interface.Services;
using Theos.Livraria.Domain.Model;
using Theos.Livraria.Domain.Model.Livro;

namespace Theos.Livraria.Api.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class LivroController : BaseController
    {

        public readonly ILivroService _livroService;
        public LivroController(ILivroService livroService, ILogger<LivroController> logger) : base(logger)
        {
            _livroService = livroService;
        }
         

        /// <summary>
        /// Obtém livro pelo Id.
        /// </summary>  
        /// <response code="200">Dados do livro.</response>
        /// <response code="400">Erros de validações.</response> 
        [HttpGet("ObterLivro/{livroId}")]
        [ProducesResponseType(typeof(BaseResponseSwagger<ResponseLivro>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterPorId(long livroId) =>
            await TratarResultadoAsync(async () =>
            {
                var resultado = await _livroService.ObterPorId(livroId);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });


        /// <summary>
        /// Obtém lista de livros.
        /// </summary>  
        /// <response code="200">Retorna lista de livro</response>
        /// <response code="400">Erros de validações.</response> 
        [HttpGet("ObterListaLivro")]
        [ProducesResponseType(typeof(BaseResponseSwagger<IEnumerable<ResponseLivro>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObterLista() =>
            await TratarResultadoAsync(async () =>
            {
                var resultado = await _livroService.ObterLista();
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });


        /// <summary>
        /// Cadastra o livro.
        /// </summary>  
        /// <response code="201">Cadastro do livro.</response>
        /// <response code="400">Erros de validações.</response> 
        /// <response code="401">Token não informado.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseSwagger<ResponseLivro>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Inserir(RequestLivro livro) =>
            await TratarResultadoAsync(async () =>
            {
                var resultado = await _livroService.Inserir(livro);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });

        /// <summary>
        /// Atualiza dados do livro
        /// </summary>  
        /// <response code="200">Atualiza livro.</response>
        /// <response code="400">Erros de validações.</response> 
        /// <response code="401">Token não informado.</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponseSwagger<ResponseLivro>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Atualizar(RequestLivro livro) =>
            await TratarResultadoAsync(async () =>
            {
                var resultado = await _livroService.Atualizar(livro);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });
         
    }
}
