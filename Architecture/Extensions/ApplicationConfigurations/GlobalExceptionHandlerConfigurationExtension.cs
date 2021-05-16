using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture
{
    public static class GlobalExceptionHandlerConfigurationExtension
    {
        public static IServiceCollection AddGlobalErrorHandler(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandlerMiddleware>();

            return services;
        }
    }
}
