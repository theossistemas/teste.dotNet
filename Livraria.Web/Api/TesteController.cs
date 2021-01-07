using Microsoft.AspNetCore.Mvc;

namespace Livraria.Web.Api
{
    [Route("api/teste")]
    public class TesteController : BaseApiController
    {

        [HttpGet]
        public ActionResult Teste()
        {
            return Ok("Teste");
        }
    }
}
