using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace TheosTestAPI.Utilities
{
    public class CustomResponse
    {
        public IActionResult Validation (string Message)
        {
            return new BadRequestObjectResult(new { success = false, message = Message });
        }

        public IActionResult Success(object Content)
        {
            return new OkObjectResult(new { success = true, content = Content });
        }
    }
}
