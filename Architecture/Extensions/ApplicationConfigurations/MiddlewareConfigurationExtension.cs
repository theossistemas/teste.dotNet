using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture
{
    public static class MiddlewareConfigurationExtension
    {
        public static IApplicationBuilder UseMiddlewareConfigurations(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();

            return app;
        }
    }
}
