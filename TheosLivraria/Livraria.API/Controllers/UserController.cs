using Livraria.Data.Repository;
using Livraria.Domain.Dto;
using Livraria.Domain.Serviços.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Livraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginDto dto)
        {
            var user = UserRepository.Get(dto.Username, dto.Password);
            if (user == null)
                return NotFound(new { message = "Usuario ou senha invalido" });

            var token = GeradorDeToken.GenerateToken(user);
            user.AlterarSenha(string.Empty);
            return new
            {
                user = UserDto.ConverterParaDto(user),
                token = token
            };
        }
    }
}
