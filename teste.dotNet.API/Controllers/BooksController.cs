using System;
using Microsoft.AspNetCore.Mvc;
using teste.dotNet.API.DTOs.Request;
using teste.dotNet.API.Services;

namespace teste.dotNet.API.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {

        private BooksService _bookService;

        public BooksController(BooksService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("{bookId}")]
        public IActionResult Get(int bookId)
        {
            try
            {
                var book = _bookService.Get(bookId);
                if (book != null)
                    return Ok(book);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet]
        [Route("")]
        public IActionResult List()
        {
            try
            {
                var books = _bookService.List();
                if (books != null && books.Count > 0)
                    return Ok(books);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult Add([FromBody] BookRequestDTO book)
        {
            try
            {
                var message = _bookService.Add(book);
                if (string.IsNullOrEmpty(message))
                    return Ok("Livro cadastrado com sucesso!");
                else
                    return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut]
        [Route("{bookId}")]
        public IActionResult Update(int bookId, [FromBody] BookRequestDTO book)
        {
            try
            {
                var message = _bookService.Update(bookId, book);
                if (string.IsNullOrEmpty(message))
                    return Ok("Livro alterado com sucesso!");
                else
                    return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpDelete]
        [Route("{bookId}")]
        public IActionResult Delete(int bookId)
        {
            try
            {
                var message = _bookService.Delete(bookId);
                if (string.IsNullOrEmpty(message))
                    return Ok("Livro deletado com sucesso!");
                else
                    return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

    }
}