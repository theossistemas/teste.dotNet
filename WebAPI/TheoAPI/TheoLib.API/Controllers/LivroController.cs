using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheoLib.Dominio.Contratos.Servicos;
using TheoLib.Dominio.Modelo;
using TheoLib.Dominio.Modelo.LivroModelo;

namespace TheoLib.API.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class LivroController : BaseController
    {
        public readonly ILivroServico _livroServico;

        public LivroController(ILivroServico livroServico, ILogger<LivroController> logger) : base(logger)
        {
            _livroServico = livroServico;
        }

        /// <summary>
        /// Pesquisa um  livro pelo Id (Chave primária).
        /// </summary>  
        /// <response code="200">OK.</response>
        /// <response code="400">Erros.</response> 
        [HttpGet("ObterPorId/{livroId}")]
        [ProducesResponseType(typeof(RespostaBaseSwagger<RespostaLivro>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaBase), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterPorId(long livroId) => await TratarResultado(async () =>
            {
                var resultado = await _livroServico.ObterPorId(livroId);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });


        /// <summary>
        /// Consulta e traz uma lista de livros.
        /// </summary>  
        /// <response code="200">Ok</response>
        /// <response code="400">Erros.</response> 
        [HttpGet("ObterLista")]
        [ProducesResponseType(typeof(RespostaBaseSwagger<IEnumerable<RespostaLivro>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaBase), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObterLista() => await TratarResultado(async () =>
            {
                var resultado = await _livroServico.ObterLista();
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });


        /// <summary>
        /// Inclui um livro.
        /// </summary>  
        /// <response code="201">Inseriu o livro.</response>
        /// <response code="400">Erros.</response> 
        /// <response code="401">Token não informado.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(RespostaBaseSwagger<RespostaLivro>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RespostaBase), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Inserir(RequisicaoLivro livro) => await TratarResultado(async () =>
            {
                var resultado = await _livroServico.Inserir(livro);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });

        /// <summary>
        /// Update do livro
        /// </summary>  
        /// <response code="200">Atualiza ok.</response>
        /// <response code="400">Erros.</response> 
        /// <response code="401">Token não informado.</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(RespostaBaseSwagger<RespostaLivro>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaBase), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Atualizar(RequisicaoLivro livro) => await TratarResultado(async () =>
            {
                var resultado = await _livroServico.Atualizar(livro);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });



    }
}
