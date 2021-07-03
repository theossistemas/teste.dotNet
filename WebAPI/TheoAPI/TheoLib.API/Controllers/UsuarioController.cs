using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheoLib.Dominio.Contratos.Servicos;
using TheoLib.Dominio.Modelo;
using TheoLib.Dominio.Modelo.UsuarioModelo;

namespace TheoLib.API.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsuarioController : BaseController
    {
        public readonly IUsuarioServico _usuarioServico;

        public UsuarioController(IUsuarioServico usuarioServico, ILogger<UsuarioController> logger) : base(logger)
        {
            _usuarioServico = usuarioServico;
        }


        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(RespostaBaseSwagger<SecurityToken>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaBase), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AutenticarUsuario([FromBody] RequisicaoLoginUsuario usuario) =>
            await TratarResultado(async () =>
            {
                var resultado = await _usuarioServico.AutenticarUsuario(usuario);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });
        

        /// <summary>
        /// Consulta Usuario Id (PrimayKey) .
        /// </summary>  
        /// <response code="200">ok.</response>
        /// <response code="400">Erros.</response> 
        [HttpGet("ObterPorId/{usuarioId}")]
        [ProducesResponseType(typeof(RespostaBaseSwagger<RespostaUsuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaBase), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterPorId(long usuarioId) =>
            await TratarResultado(async () =>
            {
                var resultado = await _usuarioServico.ObterPorId(usuarioId);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });


        /// <summary>
        /// Obtém lista de Usuarios.
        /// </summary>  
        /// <response code="200">Retorna lista de Usuario</response>
        /// <response code="400">Erros de validações.</response> 
        [HttpGet("ObterLista")]
        [ProducesResponseType(typeof(RespostaBaseSwagger<IEnumerable<RespostaUsuario>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaBase), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterLista() =>
            await TratarResultado(async () =>
            {
                var resultado = await _usuarioServico.ObterLista();
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });


        /// <summary>
        /// Inserir o Usuario.
        /// </summary>  
        /// <response code="201">Ok.</response>
        /// <response code="400">Erros.</response> 
        /// <response code="401">Token não informado.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(RespostaBaseSwagger<RespostaUsuario>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RespostaBase), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Inserir([FromBody] RequisicaoUsuario Usuario) =>
            await TratarResultado(async () =>
            {
                // Para executar apenas uma vez para cadastrar o usuário ADM com senha 123456
                //var resultado = await _usuarioServico.Inserir( new RequisicaoUsuario{ Nome = "adm",Email="adm@adm.com.br", Senha="123456" });


                var resultado = await _usuarioServico.Inserir(Usuario);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });

        /// <summary>
        /// Faz UPDATE do Usuario
        /// </summary>  
        /// <response code="200">Ok.</response>
        /// <response code="400">Erros.</response> 
        /// <response code="401">Token não informado.</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(RespostaBaseSwagger<RespostaUsuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaBase), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Atualizar([FromBody] RequisicaoUsuario usuario) =>
            await TratarResultado(async () =>
            {
                var resultado = await _usuarioServico.Atualizar(usuario);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });


    }
}
