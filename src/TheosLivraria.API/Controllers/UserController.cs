using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.User.Create.DTOs;

namespace TheosLivraria.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelo gerenciamento de usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        /// <remarks>
        /// Apenas usuários com permissão de **Admin** podem acessar este endpoint.
        /// </remarks>
        /// <param name="body">Dados necessários para criar o usuário.</param>
        /// <returns>
        /// Retorna o usuário criado em caso de sucesso.
        /// Caso contrário, retorna erros de validação ou mensagens de falha.
        /// </returns>
        /// <response code="200">Usuário criado com sucesso</response>
        /// <response code="400">Falha na criação do usuário (validação ou erro de negócio)</response>
        /// <response code="401">Não autorizado (usuário não autenticado)</response>
        /// <response code="403">Acesso negado (usuário sem permissão de Admin)</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CreateUserOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateUser(
            [FromServices] IUseCase<CreateUserInput, CreateUserOutput> usecase,
            [FromBody] CreateUserInput body)
        {
            var result = await usecase.ExecuteAsync(body);

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
