using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
