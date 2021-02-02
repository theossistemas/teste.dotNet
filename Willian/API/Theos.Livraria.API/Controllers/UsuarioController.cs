using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Theos.Livraria.Domain.Interface.Services;
using Theos.Livraria.Domain.Model;
using Theos.Livraria.Domain.Model.Usuario;

namespace Theos.Livraria.Api.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsuarioController : BaseController
    {

        public readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService, ILogger<UsuarioController> logger) : base(logger)
        {
            _usuarioService = usuarioService;
        }


        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(BaseResponseSwagger<SecurityToken>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AutenticarUsuario([FromBody] RequestLoginUsuario usuario) =>
            await TratarResultadoAsync(async () =>
            {
                var resultado = await _usuarioService.AutenticarUsuario(usuario);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });
        

        /// <summary>
        /// Obtém Usuario pelo Id.
        /// </summary>  
        /// <response code="200">Dados do Usuario.</response>
        /// <response code="400">Erros de validações.</response> 
        [HttpGet("ObterUsuario/{usuarioId}")]
        [ProducesResponseType(typeof(BaseResponseSwagger<ResponseUsuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterPorId(long usuarioId) =>
            await TratarResultadoAsync(async () =>
            {
                var resultado = await _usuarioService.ObterPorId(usuarioId);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });


        /// <summary>
        /// Obtém lista de Usuarios.
        /// </summary>  
        /// <response code="200">Retorna lista de Usuario</response>
        /// <response code="400">Erros de validações.</response> 
        [HttpGet("ObterListaUsuario")]
        [ProducesResponseType(typeof(BaseResponseSwagger<IEnumerable<ResponseUsuario>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterLista() =>
            await TratarResultadoAsync(async () =>
            {
                var resultado = await _usuarioService.ObterLista();
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });


        /// <summary>
        /// Cadastra o Usuario.
        /// </summary>  
        /// <response code="201">Cadastro do Usuario.</response>
        /// <response code="400">Erros de validações.</response> 
        /// <response code="401">Token não informado.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseSwagger<ResponseUsuario>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Inserir([FromBody] RequestUsuario Usuario) =>
            await TratarResultadoAsync(async () =>
            {
                var resultado = await _usuarioService.Inserir(Usuario);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });

        /// <summary>
        /// Atualiza dados do Usuario
        /// </summary>  
        /// <response code="200">Atualiza Usuario.</response>
        /// <response code="400">Erros de validações.</response> 
        /// <response code="401">Token não informado.</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponseSwagger<ResponseUsuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Atualizar([FromBody] RequestUsuario usuario) =>
            await TratarResultadoAsync(async () =>
            {
                var resultado = await _usuarioService.Atualizar(usuario);
                return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
            });
 
    }
}
