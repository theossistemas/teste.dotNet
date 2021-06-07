using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infra.Config
{
    public static class ConfigAutoMapper
    {
        public static void ConfigureAutoMapper(this IServiceCollection service, Type type) =>
            service.AddAutoMapper(type);
    }
}
