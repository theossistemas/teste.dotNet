using Microsoft.AspNetCore.Mvc;
using System;

namespace MMM.Library.Services.AspNetWebApi.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}")]
    public class ErrorTestController : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public string ErrorTest()
        {
           
            throw new Exception("Testando Logging para lançamento de Exception");           
            
        }
    }
}
