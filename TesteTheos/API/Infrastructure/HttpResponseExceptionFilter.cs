using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace TesteTheos.API.Infrastructure
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

                switch (context.Exception)
                {
                    case ModelNotFoundException:
                        statusCode = HttpStatusCode.NotFound;
                        break;

                    case BadRequestException:
                        statusCode = HttpStatusCode.BadRequest;
                        break;

                    default:
                        var logger = context.HttpContext.RequestServices.GetService<ILogger>();
                        logger.Error(context.Exception);
                        break;
                }

                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = (int)statusCode
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
