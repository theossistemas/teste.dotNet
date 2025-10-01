using FluentAssertions;
using FluentValidation;
using Library.Shared.Middlewares;
using Library.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Text.Json;

namespace Library.UnitTests.Shared.Middlewares;

public class GlobalExceptionHandlingTests
{
    private GlobalExceptionHandling CreateMiddleware(out Mock<ILogger<GlobalExceptionHandling>> loggerMock)
    {
        loggerMock = new Mock<ILogger<GlobalExceptionHandling>>();
        return new GlobalExceptionHandling(loggerMock.Object);
    }

    private async Task<string> InvokeMiddlewareAsync(GlobalExceptionHandling middleware, HttpContext context, Exception ex)
    {
        RequestDelegate next = (ctx) => throw ex;

        await middleware.InvokeAsync(context, next);
        context.Response.Body.Seek(0, System.IO.SeekOrigin.Begin);
        using var reader = new System.IO.StreamReader(context.Response.Body);
        return await reader.ReadToEndAsync();
    }

    [Fact]
    public async Task InvokeAsync_ShouldReturnBadRequest_ForArgumentException()
    {
        // Arrange
        var middleware = CreateMiddleware(out _);
        var context = new DefaultHttpContext();
        context.Response.Body = new System.IO.MemoryStream();

        var ex = new ArgumentException("Argument error");

        // Act
        string json = await InvokeMiddlewareAsync(middleware, context, ex);
        var result = JsonSerializer.Deserialize<ExceptionResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        context.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        result!.Message.Should().Be("Argument error");
    }

    [Fact]
    public async Task InvokeAsync_ShouldReturnUnauthorized_ForUnauthorizedAccessException()
    {
        var middleware = CreateMiddleware(out _);
        var context = new DefaultHttpContext();
        context.Response.Body = new System.IO.MemoryStream();

        var ex = new UnauthorizedAccessException("Not allowed");

        string json = await InvokeMiddlewareAsync(middleware, context, ex);
        var result = JsonSerializer.Deserialize<ExceptionResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        context.Response.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
        result!.Message.Should().Be("Não autorizado");
        result.Details.Should().ContainSingle().Which.Should().Be("Not allowed");
    }

    [Fact]
    public async Task InvokeAsync_ShouldReturnBadRequest_ForValidationException()
    {
        var middleware = CreateMiddleware(out _);
        var context = new DefaultHttpContext();
        context.Response.Body = new System.IO.MemoryStream();

        var failures = new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("Name", "Name is required")
            };
        var ex = new ValidationException(failures);

        string json = await InvokeMiddlewareAsync(middleware, context, ex);
        var result = JsonSerializer.Deserialize<ExceptionResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        context.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        result!.Message.Should().Be("Erro de validação");
        result.Details.Should().ContainSingle().Which.Should().Be("Name is required");
    }

    [Fact]
    public async Task InvokeAsync_ShouldReturnInternalServerError_ForGenericException()
    {
        var middleware = CreateMiddleware(out _);
        var context = new DefaultHttpContext();
        context.Response.Body = new System.IO.MemoryStream();

        var ex = new Exception("Generic error");

        string json = await InvokeMiddlewareAsync(middleware, context, ex);
        var result = JsonSerializer.Deserialize<ExceptionResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        result!.Message.Should().Be("Erro interno no servidor");
    }
}