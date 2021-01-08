using System;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.Dto;
using BooksApi.Models;
using BooksApi.Repositories;
using BooksApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IRepositoryUsuario _repo;
        private readonly IMapper _mapper;
        
        public UserController(IRepositoryUsuario repo, IMapper mapper, ILogger<UserController> logger)
        {
            
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async  Task<ActionResult<dynamic>> Login([FromBody]UsuarioDto model)
        {
            _logger.LogInformation(
                $"Metodo Login  : UsuarioController - > executou Dia :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");

            var user = await _repo.GetUsuario(model.Email, model.Senha);
            var result = _mapper.Map<UsuarioDto>(user);
            if (result == null)
            {
                _logger.LogWarning(
                    $"Metodo Login  : UsuarioController - > Usuario ou senha Inválidos Dia :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");

                return NotFound(new {message = "usuario ou senha inválido"});
            }

            var tocken = TokenService.GenerateToken(user);
            result.Senha = "";
            
            _logger.LogInformation(
                $"Metodo Login  : UsuarioController - > Usuario {user.Email} Logou :: Dia :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");

            return new
            {
                user = result,
                tocken = tocken
            };
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UsuarioDto userDto)
        {
            try
            {

                var existUser = _repo.UsuarioExist(userDto.Email);
               if (existUser)
                {
                    _logger.LogInformation(
                        $"Metodo Register  : UsuarioController - > Usuario {userDto.Email} tentou se registrar :: Dia :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");

                    return BadRequest("Usuario já cadastrado");
                }
                
                var user = _mapper.Map<Usuario>(userDto);
                _repo.Add(user);
                if (await _repo.SaveChangeAsync())
                {
                    _logger.LogInformation(
                        $"Metodo Register  : UsuarioController - > Usuario {userDto.Email} se registrar :: Dia :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");

                    var userAdded = _mapper.Map<UsuarioDto>(user);
                    return Created("GetUser", userAdded);
                  
                }
                _logger.LogWarning(
                    $"Metodo Register  : UsuarioController - > Usuario {userDto.Email} retornou ${BadRequest()} :: Dia :: {DateTime.Now.ToShortDateString()} : Hora:: {DateTime.Now.ToShortTimeString()}");

                return BadRequest();
            }
            catch (SqlException e)
            {
                _logger.LogError($"Error metdodo register :: {DateTime.Now.ToShortDateString()} :: {e.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}