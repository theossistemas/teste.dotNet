using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Architecture;
using AutoMapper;
using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service;

namespace IRRV.Controllers
{
    public class AccountController : BaseController<Account, AccountDTO>
    {
        protected readonly IAccountService _accountService;

        public AccountController(IAccountService accountService, IMapper mapper, ILogger<BaseController<Account, AccountDTO>> logger) : base(accountService, mapper, logger)
        {
            this._accountService = accountService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AccountDTO>> Authenticate([FromBody] LoginDTO model)
        {
            var domain = this._mapper.Map<Account>(model);
            this._logger.LogDebug(JsonConvert.SerializeObject(domain));

            var (account, jwtToken, refreshToken) = await this._accountService.AuthenticateAsync(domain, IpAddress());
            this._service.Dispose();

            var response = this._mapper.Map<AccountDTO>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken;
            this._logger.LogDebug(JsonConvert.SerializeObject(response));

            this.SetTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountInsertDTO model)
        {
            var domain = this._mapper.Map<Account>(model);
            this._logger.LogDebug(JsonConvert.SerializeObject(domain));

            var result = await this._accountService.RegisterAsync(domain, Request.Headers["origin"]);
            this._logger.LogDebug(JsonConvert.SerializeObject(result));

            this._service.Dispose();

            var response = this._mapper.Map<AccountDTO>(result);
            this._logger.LogDebug(JsonConvert.SerializeObject(response));

            return Ok(response);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(11)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"];
            }
            else
            {
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }
    }
}
