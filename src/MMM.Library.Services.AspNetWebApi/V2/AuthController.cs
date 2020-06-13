using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MMM.Library.Domain.Core.Mediator;
using MMM.Library.Domain.Core.Notifications;
using MMM.Library.Infra.CrossCutting.Identity.ApiConfiguration;
using MMM.Library.Infra.CrossCutting.Identity.ViewModels;
using MMM.Library.Services.AspNetWebApi.V1;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MMM.Library.Services.AspNetWebApi.V2
{
    // Exemplo versionamento API
    [ApiController]
    [ApiVersion("2.0", Deprecated = false)]
    [Route("v{version:apiVersion}/account")]
    public class AuthController : ApiBaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenSettings _tokenSettings;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<TokenSettings> tokenSettings,
                              INotificationHandler<DomainNotification> notifications,
                              IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenSettings = tokenSettings.Value;
        }

        [HttpPost]
        [Route("new-user")]
        public async Task<IActionResult> NewUser([FromBody] UserRegistrationViewModel userRegistration)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = userRegistration.Email,
                Email = userRegistration.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRegistration.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    NotifyError(error.Code, error.Description);
                }

                return CustomResponse(userRegistration);
            }

            await _signInManager.SignInAsync(user, false);
            var token = await JwtCreate(userRegistration.Email);

            return CustomResponse(token);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await JwtCreate(loginUser.Email));
            }

            if (result.IsLockedOut)
            {
                NotifyError("Bloqueado", "Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(loginUser);
            }
            NotifyError("403", "Usuário ou senha incorretos");

            return CustomResponse(loginUser);
        }

        private async Task<LoginResponseViewModel> JwtCreate(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.ValidOn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_tokenSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_tokenSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    // Exibir claims - develompment tests
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}
