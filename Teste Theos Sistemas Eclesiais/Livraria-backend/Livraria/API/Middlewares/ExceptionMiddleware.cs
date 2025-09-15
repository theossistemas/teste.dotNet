using System.Net;
using System.Text.Json;
using Livraria.Exceptions;

namespace Livraria.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // segue o fluxo normal
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            // Mapeamento de exceções para status HTTP
            var statusCode = exception switch
            {
                ArgumentNullException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                LivroJaCadastradoExcepton => (int)HttpStatusCode.Conflict,
                CategoriaJaExisteException => (int)HttpStatusCode.Conflict,
                CategoriaNaoEncontradaException => (int)HttpStatusCode.NotFound,
                AutenticacaoJWTException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new
            {
                error = exception.Message,
                statusCode
            });

            return response.WriteAsync(result);
        }
    }
}
