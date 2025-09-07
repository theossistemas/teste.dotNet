using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.User.Create.DTOs;

namespace TheosLivraria.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
