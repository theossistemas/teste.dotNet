using Microsoft.AspNetCore.Mvc;
using Theos.Library.Api.Controllers.Base;
using Theos.Library.Api.Models.Base;
using Theos.Library.Api.Models.Login;
using Theos.Library.Api.Security;
using Theos.Library.Core.Business.Interface;
using Theos.Library.CrossCutting.Filter.Base;
using Theos.Library.Domain.Users;

namespace Theos.Library.Api.Controllers
{
    [Route("api/v1/login")]
    [ApiController]
    public class LoginController : BaseController<User, BaseFilter, BaseModel, BaseModel, BaseModel, BaseUpdateModel>
    {
        public LoginController(IUserService service) : base(service)
        {
        }

        [HttpPost("anonymous")]
        public ActionResult Anonymous()
        {
            return Ok(UserManagement.RegisterUser());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (Validate(model))
                return GetErrors();

            var user = ((IUserService) Service).Login(model.Login, model.Password);
            if (user == null)
                return NotFound();

            return Ok(UserManagement.RegisterUser(user));
        }
    }
}
