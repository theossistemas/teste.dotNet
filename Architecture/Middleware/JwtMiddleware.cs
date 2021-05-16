using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationSettings _jwtSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<ApplicationSettings> appSettings)
        {
            _next = next;
            _jwtSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, ApplicationDataContext dataContext)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await AttachAccountToContext(context, dataContext);
            }
            else
            {
                await _next.Invoke(context);
            }

        }

        private async Task AttachAccountToContext(HttpContext context, ApplicationDataContext dataContext)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var accountId = context.AccountID(_jwtSettings);
            var account = await dataContext.Accounts.FindAsync(accountId);

            if (account == null)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }
            else
            {
                context.Items["Account"] = account;
                await _next.Invoke(context);
            }
        }
    }
}
