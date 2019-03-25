using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Theos.Library.CrossCutting.Exceptions;
using Theos.Library.CrossCutting.Response.Error;

namespace Theos.Library.Api.Helper
{
    public class HandleExceptionHelper
    {
        private readonly RequestDelegate _next;

        public HandleExceptionHelper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private static Task HandleException(HttpContext context, Exception exception)
        {
            HttpStatusCode code;
            object response = new InternalServerErrorResponseModel(exception.Message);

            switch (exception)
            {
                case PermissionException _:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case SessionException _:
                case TokenException _:
                    code = HttpStatusCode.Forbidden;
                    break;
                case UserException _:
                case EntityValidationException _:
                case VersionException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
                case UserValidationException _:
                    code = HttpStatusCode.Forbidden;
                    response = JsonConvert.DeserializeObject<object>(exception.Message);
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response,
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver {NamingStrategy = new CamelCaseNamingStrategy()},
                    Formatting = Formatting.Indented
                }));
        }
    }
}
