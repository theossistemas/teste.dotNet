using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Library.Shared.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Library.IoC.Api.Swagger;

public static class ApiServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyMethod();
                    });
        });

        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'V";
            opt.SubstituteApiVersionInUrl = true;
        });

        services.AddSwaggerGen(opt =>
        {
            var apiVersionDescriptorProvider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in apiVersionDescriptorProvider.ApiVersionDescriptions)
            {
                opt.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"Library API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString()
                });
            }
        });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddTransient<GlobalExceptionHandling>();

        return services;
    }
}