using Microsoft.AspNetCore.Mvc;
using MMM.Library.Infra.CrossCutting.Logging.AspNetFilter.CustomExeception;
using System;

namespace MMM.Library.Services.AspNetWebApi.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/error")]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error() => Problem();


        [HttpGet]
        [Route("test")]
        public string ErrorTest(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                throw new HttpResponseException();
            }
            else
            {
                return "Error Controller";
            }

        }
    }
}
