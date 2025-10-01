using Serilog;
using Library.IoC.Shared;
using Library.IoC.Api.Swagger;
using Library.IoC.Application;
using Library.IoC.CrossCutting;
using Library.IoC.Infraestructure;
using Library.Shared.Middlewares;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        IServiceCollection services = builder.Services;

        services.AddApiServices();
        services.AddApplication();
        services.AddCrossCutting();
        services.AddInfraestruucture();
        services.AddShared();

        builder.Host.UseSerilog();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<GlobalExceptionHandling>();

        app.UseAuthorization();

        app.MapControllers();

        app.UseCors("AllowAllOrigins");

        app.Run();
    }
}