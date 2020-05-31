using Livraria.Application.Apps;
using Livraria.Data.Repository;
using Livraria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Livraria.Controllers
{
    [Route("api/account")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);
            if (user == null)
                return NotFound(new { message = "Usuario ou senha invalido" });

            var token = TokenAppService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("authenticated")]
        [AllowAnonymous]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity);
    }
}