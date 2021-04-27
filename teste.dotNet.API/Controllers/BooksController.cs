using System;
using Microsoft.AspNetCore.Mvc;
using teste.dotNet.API.DTOs.Request;
using teste.dotNet.API.Services;

namespace teste.dotNet.API.Controllers {
    [Route("api/[controller]")]
    public class BooksController : ControllerBase {

        private BooksService _bookService;

        public BooksController(BooksService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("{bookId}")]
        public IActionResult Get(int bookId) {
            var book = _bookService.Get(bookId);
            if(book != null)
                return Ok(book);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("")]
        public IActionResult List() {
            var books = _bookService.List();
            if(books != null && books.Count > 0)
                return Ok(books);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("")]
        public IActionResult Add([FromBody] BookRequestDTO book) {
            try{
                _bookService.Add(book);
                return Ok();
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{bookId}")]
        public IActionResult Update(int bookId, [FromBody] BookRequestDTO book) {
             try{
                _bookService.Update(bookId, book);
                return Ok();
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

    }
}