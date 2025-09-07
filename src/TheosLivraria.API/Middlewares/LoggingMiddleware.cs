using System.Text;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext ctx, ILogService logService)
    {
        ctx.Request.EnableBuffering();
        string requestBody = string.Empty;

        using (var reader = new StreamReader(ctx.Request.Body, Encoding.UTF8, leaveOpen: true))
        {
            requestBody = await reader.ReadToEndAsync();
            ctx.Request.Body.Position = 0;
        }

        var originalBody = ctx.Response.Body;
        using var newBody = new MemoryStream();
        ctx.Response.Body = newBody;

        await _next(ctx);

        newBody.Seek(0, SeekOrigin.Begin);
        string responseBody = await new StreamReader(newBody).ReadToEndAsync();
        newBody.Seek(0, SeekOrigin.Begin);

        await logService.SaveRequestAsync(ctx.Request.Path, ctx.Request.Method, requestBody, responseBody, ctx.Response.StatusCode);

        ctx.Response.Body = originalBody;
        await newBody.CopyToAsync(originalBody);
    }
}
