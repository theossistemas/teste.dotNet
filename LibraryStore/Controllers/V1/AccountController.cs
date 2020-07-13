using LibraryStore.Core.Business;
using LibraryStore.Core.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryStore.Controllers.v1
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountBusiness business;

        public AccountController(IAccountBusiness business)
        {
            this.business = business;
        }

        /// <summary>
        /// Authenticates the user informed to access restricted actions
        /// </summary>
        /// <param name="dto">Authentication data</param>
        /// <returns>Specific user and autorization token</returns>
        /// <response code="200">Returns the specific user</response>
        /// <response code="404">If the invalid username or password</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("login")]
        [Produces("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginInputDto dto)
        {
            var user = await business.Authenticate(dto);
            if (user == null)
                return NotFound();

            return user;
        }
    }
}