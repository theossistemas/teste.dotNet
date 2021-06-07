using Domain.Interface;
using Domain.Model.Book;
using LoggerService.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookService _bookService { get; }
        private ILoggerManager _logger { get; }
        public BookController(IBookService bookService, ILoggerManager logger)
        {
            _bookService = bookService;
            _logger = logger;
        }


        /// <summary>
        /// Método responsável por retornar todos os livros cadastrados, ordenados por nome - Acesso Livre
        /// </summary>
        /// <returns>Todos os livros cadastrados, ordenados por nome.</returns>
        /// <response code="200">Lista de todos os livros cadastrados.</response>
        /// <response code="404">Não possui registros a exibir.</response>
        /// <response code="500">Exceção não tratada.</response>
        [HttpGet("GetAll")]
        [AllowAnonymous]
        public IActionResult GetBooks()
        {
            try
            {
                var books = _bookService.GetAllBooks();

                if (!books.Any())
                    return NotFound();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro não tratado na classe BookController - método GetBooks: ${ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao buscar dados.");
            }
        }


        /// <summary>
        /// Método responsável por cadastrar um novo livro - Necessário permissão de Administrador
        /// </summary>
        /// <remarks>
        /// Exemplo:
        /// 
        ///     POST
        ///     {
        ///         "title": "Matrix",
        ///         "genre": "Action"
        ///     }
        /// </remarks>
        /// <param name="book">Objeto contendo as informações do livro a cadastrar.</param>
        /// <returns>O objeto recém criado.</returns>
        /// <response code="201">Objeto criado.</response>
        /// <response code="400">Se as informações do objeto forem inválidas ou se o nome do livro já estiver cadastrado.</response>
        /// <response code="500">Exceção não tratada.</response>
        [HttpPost]
        [Authorize]
        public IActionResult AddBook([FromBody] CreateBookModel book)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Objeto inválido.");

                var created = _bookService.AddBook(book);

                if (created == null)
                    return BadRequest("Livro já cadastrado.");

                _logger.LogInfo($"Novo livro cadastrado: {DateTime.Now} | {book.Title}");

                return CreatedAtRoute("AddBook", book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"\nErro não tratado na classe BookController - método AddBook: ${ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao tentar inserir registro.");
            }
        }

        /// <summary>
        /// Método responsável por editar um livro cadastrado - Necessário permissão de Administrador
        /// </summary>
        /// <remarks>
        /// Exemplo:
        /// 
        ///     PUT 
        ///     {
        ///         "title": "Adam Sandler",
        ///         "genre": "Comedy"
        ///     }
        /// </remarks>
        /// <param name="book">Objeto contendo as informações atualizadas.</param>
        /// <param name="id">Identificador do livro a ser atualizado.</param>
        /// <returns>Não possui conteúdo de resposta.</returns>
        /// <response code="201"></response>
        /// <response code="400">Se as informações do objeto forem inválidas, o nome do livro já estiver cadastrado ou se o livro não for encontrado na base de dados.</response>
        /// <response code="500">Exceção não tratada.</response>
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateBook([FromBody] UpdateBookModel book, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Objeto inválido.");

                var updatedBook = _bookService.UpdateBook(book, id);

                if (updatedBook == null)
                    return BadRequest("Livro já cadastrado.");

                if (updatedBook.Title == string.Empty)
                    return BadRequest("Livro não encontrado na base de dados.");

                _logger.LogInfo($"Livro ID {id} atualizado: {DateTime.Now} | \n ANTIGO: {book.Title} NOVO: {updatedBook.Title}");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"\nErro não tratado na classe BookController - método UpdateBook: ${ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao tentar atualizar registro.");
            }
        }

        /// <summary>
        /// Método responsável por excluir um livro cadastrado - Necessário permissão de Administrador
        /// </summary>
        /// <param name="id">Identificador do livro a ser excluído.</param>
        /// <returns>Não possui conteúdo de resposta.</returns>
        /// <response code="201"></response>
        /// <response code="400">Livro não encontrado na base de dados.</response>
        /// <response code="500">Exceção não tratada.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var deleted = _bookService.DeleteBook(id);

                if (!deleted)
                    return BadRequest("Livro não encontrado na base de dados.");

                _logger.LogInfo($"Livro ID {id} excluído: {DateTime.Now}");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"\nErro não tratado na classe BookController - método DeleteBook: ${ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao tentar excluir registro.");
            }
        }
    }
}
