using Library.Shared.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Library.Shared.Middlewares;

public class GlobalExceptionHandling(ILogger<GlobalExceptionHandling> logger) : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandling> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Erro na aplicação. | Rota => {1} | classe => {2} | metodo => {3}",
                context.Request.Path.Value,
                exception.TargetSite!.DeclaringType?.Name,
                exception.TargetSite?.Name);

            List<string> erros = [];

            if (exception is ValidationException fluentException)
            {
                foreach (var error in fluentException.Errors)
                    erros.Add(error.ErrorMessage);
            }

            ExceptionResponse exceptionResult = exception switch
            {
                ArgumentException _ => new ExceptionResponse(HttpStatusCode.BadRequest, exception.Message, erros),
                UnauthorizedAccessException _ => new ExceptionResponse(HttpStatusCode.Unauthorized, "Não autorizado", [exception.Message]),
                InvalidOperationException when exception.Message.Contains("Authorization") => new ExceptionResponse(HttpStatusCode.Unauthorized, exception.Message),
                ValidationException _ => new ExceptionResponse(HttpStatusCode.BadRequest, "Erro de validação", erros),
                FormatException _ => new ExceptionResponse(HttpStatusCode.BadRequest, exception.Message),
                _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Erro interno no servidor")
            };

            context.Response.StatusCode = (int)exceptionResult.Code;
            context.Response.ContentType = "application/json";
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(exceptionResult, options);
            await context.Response.WriteAsync(json);
        }
    }
}