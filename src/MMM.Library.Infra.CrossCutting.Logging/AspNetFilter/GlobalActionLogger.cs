using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace MMM.Library.Infra.CrossCutting.Logging.AspNetFilter
{
    public class GlobalActionLogger : IActionFilter
    {
        private readonly ILogger<GlobalActionLogger> _logger;
        private readonly IHostEnvironment _hostingEnviroment;
        private SystemInfo SystemInfo { get; }

        public GlobalActionLogger(ILogger<GlobalActionLogger> logger,
                                  IHostEnvironment hostingEnviroment,
                                  IOptions<SystemInfo> systemInfo)
        {
            _logger = logger;
            _hostingEnviroment = hostingEnviroment;
            SystemInfo = systemInfo.Value;
        }

        // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-3.1
        // Do something after the action executes.
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_hostingEnviroment.IsDevelopment())
            {
                var data = new
                {
                    Version = SystemInfo.SystemVersion,
                    Application = SystemInfo.SystemVersion,
                    Source = "GlobalActionLoggerFilter",
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

                DevTools.PrintConsoleMessage("Global Logger with Action Filter:::::::::::::::::::::::::::::::::::::::::",
                    ConsoleColor.Yellow, ConsoleColor.Red);
                DevTools.PrintConsoleMessage(data.ToString(), ConsoleColor.DarkBlue, ConsoleColor.Gray);
                DevTools.PrintConsoleMessage("-------------------------------------------------------------------------",
                  ConsoleColor.Yellow, ConsoleColor.Red);

                //string data1 = data.ToString();
                _logger.LogInformation(1, " ", "Parameters: {data}", data.ToString());
            }
        }

        // Do something before the action executes.
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
