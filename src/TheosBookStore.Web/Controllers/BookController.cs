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

        public BookController(IBookRegister bookRegister)
        {
            _bookRegister = bookRegister;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDefault<BookInsertRequest>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseDefault<BookInsertRequest>), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] BookInsertRequest bookRequest)
        {
            var response = new ResponseDefault<BookInsertRequest>();
            _bookRegister.Register(bookRequest);
            if (!_bookRegister.IsValid)
            {
                response.message = _bookRegister.GetErrorMessages();
                return BadRequest(response);
            }
            response.Data = bookRequest;
            return Ok(response);
        }
    }
}
