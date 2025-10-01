using Serilog;
using Microsoft.Extensions.DependencyInjection;

namespace Library.IoC.Shared;

public static class SharedIoc
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        AddLogs(services);
        return services;
    }

    public static void AddLogs(IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Minute)
            .CreateLogger();
    }
}