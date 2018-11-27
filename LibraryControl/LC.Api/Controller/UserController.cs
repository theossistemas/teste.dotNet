using System;
using System.Collections.Generic;
using System.Linq;
using LC.Application.User;
using LC.Application.User.DataTransferObject;
using LC.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LC.Api.Controller
{
    [Route("api/1.0/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        /// <summary>
        /// Buscar todos os usuarios
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get([FromServices] ApplicationUser app)
        {
            try
            {
                return Ok(app.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Realizar autenticação
        /// </summary>
        /// <param name="userLoginDTO"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<TokenDTO> Login(UserLoginDTO userLoginDTO , [FromServices] ApplicationUser app)
        {
            try
            {
                return Ok(app.Login(userLoginDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}