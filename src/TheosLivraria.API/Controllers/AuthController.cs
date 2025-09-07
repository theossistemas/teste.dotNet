using Microsoft.AspNetCore.Mvc;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.User.Auth.DTOs;

namespace TheosLivraria.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromServices] IUseCase<AuthUserInput, AuthUserOutput> usecase, [FromBody] AuthUserInput input)
        {
            var result = await usecase.ExecuteAsync(input);

            if (result.Success)
            {
                return Ok(result.Value);
            }

            return BadRequest(new { error = result.ErrorMessage, details = result.MetaData });
        }
    }
}
