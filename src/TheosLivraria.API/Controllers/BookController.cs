using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.Book.Create.DTOs;
using TheosLivraria.Application.UseCases.Book.GetById.DTOs;
using TheosLivraria.Application.UseCases.Book.list.DTOs;
using TheosLivraria.Application.UseCases.Book.Update.DTOs;

namespace TheosLivraria.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IUseCase<CreateBookInput, CreateBookOutput> _createBookUseCase;
        private readonly IUseCase<UpdateBookInput, UpdateBookOutput> _updateBookUseCase;
        private readonly IUseCase<ListBooksInput, ListBooksOutput> _listBooksUseCase;
        private readonly IUseCase<GetBookByIdInput, GetBookByIdOutput> _getBookByIdUseCase;

        public BookController(
            IUseCase<CreateBookInput, CreateBookOutput> createBookUseCase,
            IUseCase<UpdateBookInput, UpdateBookOutput> updateBookUseCase,
            IUseCase<ListBooksInput, ListBooksOutput> listBooksUseCase,
            IUseCase<GetBookByIdInput, GetBookByIdOutput> getBookByIdUseCase)
        {
            _createBookUseCase = createBookUseCase;
            _updateBookUseCase = updateBookUseCase;
            _listBooksUseCase = listBooksUseCase;
            _getBookByIdUseCase = getBookByIdUseCase;
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

        /// <summary>
        /// Lista livros com paginação
        /// </summary>
        /// <param name="page">Número da página (começa em 1)</param>
        /// <param name="pageSize">Quantidade de itens por página</param>
        /// <returns>Lista paginada de livros</returns>
        [HttpGet("books")]
        [AllowAnonymous]
        public async Task<IActionResult> ListBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var input = new ListBooksInput
            {
                Page = page,
                PageSize = pageSize
            };

            var result = await _listBooksUseCase.ExecuteAsync(input);

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
        /// Busca um livro pelo Id
        /// </summary>
        /// <param name="id">Id do livro</param>
        /// <returns>Livro encontrado</returns>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var input = new GetBookByIdInput { Id = id };
            var result = await _getBookByIdUseCase.ExecuteAsync(input);

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
