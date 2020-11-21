using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TheosTestAPI.Entity;
using TheosTestAPI.DAO;
using TheosTestAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace TheosTestAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Properties

        protected readonly CustomResponse _response;

        private readonly IConfiguration _config;

        private readonly ILogger<AuthController> _logger;

        #endregion Properties



        #region Routes

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] LoginDetails loginDetails)
        {
            try
            {
                if (loginDetails.Login == null)
                {
                    return _response.Validation("Login não pode estar vazio");
                }
                if (loginDetails.Password == null)
                {
                    return _response.Validation("Senha não pode estar vazia");
                }

                bool CredentialsAreValid = ValidateCredentials(loginDetails);
                if (CredentialsAreValid)
                {
                    using (TheosTestContext context = new TheosTestContext())
                    {
                        Admin admin = context.Set<Admin>()
                            .Where(a => a.Login.Equals(loginDetails.Login)).FirstOrDefault();

                        admin.Password = "";

                        CriptoAssistant assit = new CriptoAssistant();

                        string tokenString = assit.GenerateNewJWT(_config);

                        _logger.LogInformation("Login com sucesso");

                        return _response.Success(new { user = admin, token = tokenString });
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                return HandleError(e);
            }
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult VerifyToken()
        {
            return _response.Success(null);
        }

        #endregion Routes



        #region Other Methods

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger)
        {
            _config = configuration;
            _response = new CustomResponse();
            _logger = logger;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private bool ValidateCredentials(LoginDetails loginDetails)
        {
            using (TheosTestContext context = new TheosTestContext())
            {
                CriptoAssistant cript = new CriptoAssistant();

                Admin admin = context.Set<Admin>()
                    .Where(a => a.Login.Equals(loginDetails.Login)
                    && a.Password.Equals(cript.hideString(loginDetails.Password))
                    ).FirstOrDefault();

                return (admin != null);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError(Exception e)
        {
            _logger.LogError(e.Message);

            return _response.Validation("Ocorreu um erro desconhecido executando a operação");
        }

        #endregion Other Methods
    }
}
