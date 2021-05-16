using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
		private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
			this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next.Invoke(context);
			}
			catch (Exception ex)
			{
				this._logger.LogError(JsonConvert.SerializeObject(ex));
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = MediaTypeNames.Application.Json;
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var json = new
			{
				message = exception.Message
			};

			switch (exception)
			{
				case ApplicationException e:
				case ValidationException e1:
				case FluentValidation.ValidationException e2:
					context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					break;
				case KeyNotFoundException e:
					context.Response.StatusCode = (int)HttpStatusCode.NotFound;
					break;
				case UnauthorizedAccessException e:
					context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
					break;
				case TokenExperidedException e:
					context.Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
					break;
				default:
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					break;
			}
			
			return context.Response.WriteAsync(JsonConvert.SerializeObject(json));
		}
	}
}
