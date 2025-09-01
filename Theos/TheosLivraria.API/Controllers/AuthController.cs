using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.Application.UseCases.User.CreateAdmin;
using TheosLivraria.Application.UseCases.User.GetLogin;
using TheosLivraria.Application.UseCases.ViewModels.Request.User.CreateAdmin;
using TheosLivraria.Application.UseCases.ViewModels.Request.User.GetLogin;
using TheosLivraria.Application.UseCases.ViewModels.Response.User.CreateAdmin;
using TheosLivraria.Application.UseCases.ViewModels.Response.User.Login;

namespace TheosLivraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <remarks>
        /// This method is an HTTP POST endpoint. It receives the user's credentials,
        /// validates them, and returns a JWT token if the login is successful.
        /// Public users can access this endpoint without authentication.
        /// </remarks>
        /// <param name="useCase">The login use case service injected to execute the authentication logic.</param>
        /// <param name="request">The request model containing username and password.</param>
        /// <returns>Returns a ResponseLoginViewModel containing the JWT token, username, and role with HTTP 200 OK.</returns>
        [ProducesResponseType(typeof(ResponseLoginViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromServices] ILoginUseCase useCase ,[FromBody] RequestLoginViewModel request)
        {
            var response = await useCase.ExecuteAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new administrator user.
        /// </summary>
        /// <remarks>
        /// This method is an HTTP POST endpoint. It receives a request with the admin's username and password,
        /// validates if the username is already registered, hashes the password, and persists the admin user in the database.
        /// Only users with the "Admin" role should be allowed to access this endpoint.
        /// </remarks>
        /// <param name="userCase">The use case service injected to execute the admin creation logic.</param>
        /// <param name="request">The request model containing the username and password for the new admin.</param>
        /// <returns>Returns a ResponseCreateAdminViewModel containing the new admin's ID, username, and role with HTTP 201 Created.</returns>
        [ProducesResponseType(typeof(ResponseCreateAdminViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromServices] ICreateAdminUserCase userCase, RequestCreateAdminViewModel request)
        {
            return Created(string.Empty, await userCase.ExecuteAsync(request));
        }
    }
}
