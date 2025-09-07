using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.Book.Create.DTOs;
using TheosLivraria.Application.UseCases.Book.Update.DTOs;

namespace TheosLivraria.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IUseCase<CreateBookInput, CreateBookOutput> _createBookUseCase;
        private readonly IUseCase<UpdateBookInput, UpdateBookOutput> _updateBookUseCase;

        public BookController(
            IUseCase<CreateBookInput, CreateBookOutput> createBookUseCase,
            IUseCase<UpdateBookInput, UpdateBookOutput> updateBookUseCase)
        {
            _createBookUseCase = createBookUseCase;
            _updateBookUseCase = updateBookUseCase;
        }

        /// <summary>
        /// Cria um novo livro
        /// </summary>
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

        /// <summary>
        /// Atualiza um livro existente
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Update([FromBody] UpdateBookInput input)
        {
            var result = await _updateBookUseCase.ExecuteAsync(input);

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
