using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.Book.Create;
using TheosLivraria.Application.UseCases.Book.Create.DTOs;
using TheosLivraria.Application.UseCases.Commom;

namespace TheosLivraria.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IUseCase<CreateBookInput, CreateBookOutput> _createBookUseCase;

        public BookController(IUseCase<CreateBookInput, CreateBookOutput> createBookUseCase)
        {
            _createBookUseCase = createBookUseCase;
        }

        /// <summary>
        /// Cria um novo livro
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create([FromBody] CreateBookInput input)
        {
            var result = await _createBookUseCase.ExecuteAsync(input);

            if (!result.Success)
            {
                return BadRequest(new
                {
                    Message = result.ErrorMessage,
                    Errors = result.MetaData
                });
            }

            return Ok(result.Value);
        }
    }
}
