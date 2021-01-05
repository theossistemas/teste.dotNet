using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Web.Api
{
    [ApiController, Authorize]
    public class BaseApiController : ControllerBase
    {

    }
}