using MediatR;
using FluentValidation;
using Library.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace Library.IoC.Application;

public static class ApplicationIoC
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(ValidationBehaviour<,>).Assembly));
        services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}