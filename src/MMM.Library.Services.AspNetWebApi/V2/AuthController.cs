using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMM.Library.Domain.Core.Mediator;
using MMM.Library.Domain.Core.Notifications;
using MMM.Library.Infra.CrossCutting.Identity.Services;
using MMM.Library.Infra.CrossCutting.Identity.ViewModels;
using MMM.Library.Services.AspNetWebApi.V1;
using System;
using System.Threading.Tasks;

namespace MMM.Library.Services.AspNetWebApi.V2
{
    [ApiController]
    [ApiVersion("2.0", Deprecated = false)]
    [Route("v{version:apiVersion}/account")]
    public class AuthController : ApiBaseController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IIdentityService _identityService;

        public AuthController(ILogger<AuthController> logger,
                              IIdentityService identityService,
                              INotificationHandler<DomainNotification> notifications,
                              IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _logger = logger;
            _identityService = identityService;
        }

        [HttpPost]
        [Route("new-user")]
        public async Task<IActionResult> NewUser(UserRegistrationViewModel userRegistration)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _identityService.NewUser(userRegistration);

            if (result)
            {
                var responseNewUser = await _identityService.JwtGenerate(userRegistration.Email);

                _logger.LogInformation(DateTime.Now.ToLongDateString() + " - Usuário criado com sucesso: " + userRegistration.Email);

                return CustomResponse(responseNewUser);
            }

            _logger.LogInformation(DateTime.Now.ToLongDateString() + " - Falha ao criar Usuário: " + userRegistration.Email);

            return CustomResponse(userRegistration);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _identityService.Login(loginUser);

            if (result)
            {
                var responseLogin = await _identityService.JwtGenerate(loginUser.Email);

                _logger.LogInformation(DateTime.Now.ToLongDateString() + " - Login realizado com sucesso para usuário: " + loginUser.Email);

                return CustomResponse(responseLogin);
            }

            _logger.LogInformation(DateTime.Now.ToLongDateString() + " - Tentativa de Login falhou para usuário: " + loginUser.Email);
            return CustomResponse(loginUser);
        }

    }
}