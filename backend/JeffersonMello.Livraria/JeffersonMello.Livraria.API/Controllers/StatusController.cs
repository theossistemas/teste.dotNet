using JeffersonMello.Livraria.Common.Response;
using Microsoft.AspNetCore.Mvc;

namespace JeffersonMello.Livraria.API.Controllers
{
    [ApiController, Route("Status")]
    public class StatusController : ControllerBase
    {
        #region Public Methods

        /// <summary>
        /// Indica se a API está em funcionamento
        /// </summary>
        /// <returns>Response, Succes = true se estiver tudo ok</returns>
        [Route("Alive"), HttpGet, HttpOptions]
        public Response Alive()
        {
            return new Response
            {
                Success = true,
                Message = "Server is Alive!",
                Data = null,
            };
        }

        #endregion Public Methods
    }
}