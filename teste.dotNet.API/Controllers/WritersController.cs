using Microsoft.AspNetCore.Mvc;
using teste.dotNet.API.Services;

namespace teste.dotNet.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class WritersController : ControllerBase {

        private WritersService _writerService;

        public WritersController(WritersService writerService)
        {
            _writerService = writerService;
        }
        
    }
}