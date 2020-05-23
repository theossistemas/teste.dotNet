using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.App.Services;
using TheosBookStore.Web.Models;

namespace TheosBookStore.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRegister _bookRegister;
        private readonly IBookUpdater _bookUpdater;
        private readonly IBookRemover _bookRemover;
        private readonly IBookList _bookList;

        public BookController(IBookRegister bookRegister, IBookUpdater bookUpdater,
            IBookRemover bookRemover, IBookList bookList)
        {
            _bookRegister = bookRegister;
            _bookUpdater = bookUpdater;
            _bookRemover = bookRemover;
            _bookList = bookList;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDefault<ICollection<BookResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDefault<ICollection<BookResponse>>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseDefault<ICollection<BookResponse>>), StatusCodes.Status400BadRequest)]
        public IActionResult GetOrderedByTitle([FromQuery] int? page,
            [FromQuery] int? qty)
        {
            var response = new ResponseDefault<ICollection<BookResponse>>();
            if (!page.HasValue || page <= 0)
                page = 1;
            if (!qty.HasValue || qty <= 0)
                qty = 10;
            var offset = (page - 1) * qty;

            var bookList = _bookList.GetOrderedByTitle(qty.Value, offset.Value);
            if (!_bookList.IsValid)
            {
                response.message = _bookList.GetErrorMessages();
                return BadRequest(response);
            }

            if (bookList.Count == 0)
                return NoContent();

            response.Data = bookList;
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDefault<BookResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseDefault<BookResponse>), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] BookRequest bookRequest)
        {
            var response = new ResponseDefault<BookResponse>();
            var bookResponse = _bookRegister.Execute(bookRequest);
            if (!_bookRegister.IsValid)
            {
                response.message = _bookRegister.GetErrorMessages();
                return BadRequest(response);
            }
            response.Data = bookResponse;
            return Created("", response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseDefault<BookResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDefault<BookResponse>), StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromBody] BookRequest bookRequest)
        {
            var response = new ResponseDefault<BookResponse>();
            var bookResponse = _bookUpdater.Execute(bookRequest);
            if (!_bookUpdater.IsValid)
            {
                response.message = _bookUpdater.GetErrorMessages();
                return BadRequest(response);
            }
            response.Data = bookResponse;
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseDefault<BookResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDefault<BookResponse>), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            var response = new ResponseDefault<BookResponse>();
            var bookRequest = new BookRequest { Id = id };
            var bookResponse = _bookRemover.Execute(bookRequest);
            if (!_bookRemover.IsValid)
            {
                response.message = _bookRemover.GetErrorMessages();
                return BadRequest(response);
            }
            response.Data = bookResponse;
            return Ok(response);
        }
    }
}
