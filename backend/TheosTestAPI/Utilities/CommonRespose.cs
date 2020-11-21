using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheosTestAPI.Utilities
{
    public class CommonRespose
    {
        public IActionResult Error(string Message)
        {
            return new BadRequestObjectResult(new { success = false, message = Message });
        }

        public IActionResult Success(string Message)
        {
            return new BadRequestObjectResult(new { success = false, message = Message });
        }
    }
}
