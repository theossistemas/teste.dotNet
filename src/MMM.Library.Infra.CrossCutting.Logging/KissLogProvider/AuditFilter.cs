using KissLog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace MMM.Library.Infra.CrossCutting.Logging.KissLogProvider
{
    public class AuditFilter : IActionFilter
    {
        private readonly ILogger _logger;
        private readonly IHostEnvironment _hostingEnviroment;
        private readonly IConfiguration _configuration;

        public AuditFilter(ILogger logger, IHostEnvironment hostingEnviroment, IConfiguration configuration)
        {
            _logger = logger;
            _hostingEnviroment = hostingEnviroment;
            _configuration = configuration;
        }

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_hostingEnviroment.IsDevelopment())
            {
                var data = new
                {
                    System = _configuration["System.Name"],
                    Version = _configuration["System.Version"],
                    Application = _hostingEnviroment.ApplicationName,
                    Source = "AuditFilter",
                    User = context.HttpContext.User.Identity.Name,
                    Hostname = context.HttpContext.Request.Host.ToString(),
                    IP = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Url = context.HttpContext.Request.GetDisplayUrl(),
                    Method = context.HttpContext.Request.Method,
                    StatusCode = context.HttpContext.Response.StatusCode,
                    AreaAccessed = context.HttpContext.Request.GetDisplayUrl(),
                    Action = context.ActionDescriptor.DisplayName,
                    TimeStamp = DateTime.Now,
                };

                DevTools.PrintConsoleMessage(data.ToString(), ConsoleColor.DarkBlue, ConsoleColor.Gray);

                // Log examples (http://kisslog.net):
                //_logger.Info(data);

                //_logger.Debug("log example for Debug");
                //_logger.Warn("log example for Warn");
                //_logger.Critical("log example for Critical");
                //_logger.Trace("log example for Trace");
                //_logger.Error("log example for Error");
            }
        }
    }
}
