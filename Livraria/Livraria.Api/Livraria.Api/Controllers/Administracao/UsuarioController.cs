using AutoMapper;
using Livraria.Api.Dto;
using Livraria.Domain.Dto.Administracao;
using Livraria.Domain.Entities.Administracao;
using Livraria.Services.Interfaces.Administracao;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Livraria.Api.Controllers.Administracao
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerApiBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;


        public UsuarioController(IUsuarioService usuarioService, ITokenService tokenService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult> Registrar([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = _mapper.Map<UsuarioDto, Usuario>(usuarioDto);
                await _usuarioService.Registrar(usuario);
                return CreatedAtAction(nameof(Registrar), new { id = usuario.Id }, usuarioDto);
            }
            catch (AggregateException aex)
            {
                return BadRequest(new ErroResponse(aex.InnerExceptions));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErroResponse(ex));
            }
        }

        [HttpPost("Autenticar")]
        public async Task<ActionResult<dynamic>> Autenticar([FromBody] LoginDto loginDto)
        {
            try
            {
                var usuario = await _usuarioService.Autenticar(loginDto.Email, loginDto.Senha);
                if (usuario != null)
                {
                    var token = _tokenService.GenerateToken(usuario);

                    return Ok(new
                    {
                        login = usuario.Email,
                        token
                    });
                }
                return Unauthorized("Credenciais inválidas.");
            }
            catch (AggregateException aex)
            {
                return BadRequest(new ErroResponse(aex.InnerExceptions));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErroResponse(ex));
            }
        }
    }
}
