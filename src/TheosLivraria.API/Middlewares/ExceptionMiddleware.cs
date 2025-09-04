public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext ctx, ILogService logService)
    {
        try
        {
            await _next(ctx);
        }
        catch (Exception ex)
        {
            await logService.SaveExceptionAsync(ctx.Request.Path, ctx.Request.Method, ex);

            ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
            ctx.Response.ContentType = "application/json";
            await ctx.Response.WriteAsJsonAsync(new { Message = "Erro interno" });
        }
    }
}
