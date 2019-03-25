using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Theos.Library.Api.Controllers;
using Theos.Library.Api.Models.Login;
using Theos.Library.Api.Security;
using Theos.Library.Core.Business;
using Theos.Library.Core.Data.Repository;
using Theos.Library.CrossCutting.Response.Error;
using Xunit;

namespace Theos.Library.Api.Test
{
    public class LoginControllerTest
    {
        private LoginController _loginController;

        public LoginControllerTest()
        {
            var repository = new UserRepository();
            var service = new UserService(repository);
            _loginController = new LoginController(service);
        }

        [Fact]
        public void Ok_WhenCalled_LoginAnonymous()
        {
            var result = _loginController.Anonymous();
            Assert.True(result is OkObjectResult);

            var okResult = (OkObjectResult)result;
            Assert.True(okResult.Value is LoginResponseModel);

            var resultModel = (LoginResponseModel)okResult.Value;
            Assert.Equal(resultModel.Login, ProfileEnum.Anonymous.ToString());
            Assert.NotEqual(resultModel.Token, Guid.Empty);
        }

        [Fact]
        public void Ok_WhenCalled_Login()
        {
            var model = new LoginModel
            {
                Login = "admin",
                Password = "admin"
            };

            var result = _loginController.Login(model);
            Assert.True(result is OkObjectResult);

            var okResult = (OkObjectResult)result;
            Assert.True(okResult.Value is LoginResponseModel);

            var resultModel = (LoginResponseModel)okResult.Value;
            Assert.Equal(resultModel.Login, model.Login);
            Assert.NotEqual(resultModel.Token, Guid.Empty);
        }

        [Fact]
        public void BadRequest_WhenCalled_Login_WithoutLogin()
        {
            var model = new LoginModel
            {
                Password = "admin"
            };

            var result = _loginController.Login(model);
            Assert.True(result is BadRequestObjectResult);

            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseModel);

            var resultModel = (ErrorResponseModel)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Login"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
        }

        [Fact]
        public void BadRequest_WhenCalled_Login_WithoutPassword()
        {
            var model = new LoginModel
            {
                Login = "admin"
            };

            var result = _loginController.Login(model);
            Assert.True(result is BadRequestObjectResult);

            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseModel);

            var resultModel = (ErrorResponseModel)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Password"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
        }

        [Fact]
        public void BadRequest_WhenCalled_Login_Without_Login_and_Password()
        {
            var model = new LoginModel();

            var result = _loginController.Login(model);
            Assert.True(result is BadRequestObjectResult);

            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseModel);

            var resultModel = (ErrorResponseModel)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 2);
        }

        [Fact]
        public void NotFound_WhenCalled_Login_WithIncorrect_Credentials()
        {
            var model = new LoginModel
            {
                Login = "123",
                Password = "123"
            };

            var result = _loginController.Login(model);
            Assert.True(result is NotFoundResult);
        }

    }
}
