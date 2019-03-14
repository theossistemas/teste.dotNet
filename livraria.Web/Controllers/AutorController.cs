using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using livraria.Application.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace livraria.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : Controller
    {
        private readonly IAutorApplication _autorApplication;

        public AutorController(IAutorApplication autorApplication)
        {
            _autorApplication = autorApplication;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            try
            {
                var result = _autorApplication.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}