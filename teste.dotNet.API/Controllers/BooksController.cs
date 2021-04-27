using Microsoft.AspNetCore.Mvc;
using teste.dotNet.API.Services;

namespace teste.dotNet.API.Controllers {
    [Route("api/[controller]")]
    public class BooksController : ControllerBase {

        private BooksService _bookService;

        public BooksController(BooksService bookService)
        {
            _bookService = bookService;
        }

    }
}