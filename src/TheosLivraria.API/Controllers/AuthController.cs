using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.User.Auth.DTOs;

namespace TheosLivraria.API.Controllers
{
    /// <summary>
    /// Controlador responsável pela autenticação de usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Realiza o login de um usuário e retorna um token JWT.
        /// </summary>
        /// <remarks>
        /// Este endpoint é utilizado para autenticação de usuários no sistema.
        /// Envie as credenciais (e-mail e senha) no corpo da requisição.
        /// </remarks>
        /// <param name="input">Credenciais do usuário (e-mail e senha).</param>
        /// <returns>
        /// Retorna o token JWT e informações do usuário em caso de sucesso.
        /// Caso contrário, retorna erros de autenticação ou validação.
        /// </returns>
        /// <response code="200">Autenticação realizada com sucesso</response>
        /// <response code="400">Falha na autenticação (credenciais inválidas ou erro de validação)</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthUserOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(
            [FromServices] IUseCase<AuthUserInput, AuthUserOutput> usecase,
            [FromBody] AuthUserInput input)
        {
            var result = await usecase.ExecuteAsync(input);

            if (result.Success)
            {
                return Ok(result.Value);
            }

            return BadRequest(new
            {
                error = result.ErrorMessage,
                details = result.MetaData
            });
        }
    }
}
