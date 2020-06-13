using KissLog;
using KissLog.Apis.v1.Listeners;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MMM.Library.Services.AspNetWebApi.Configurations
{
    public static class LogSetup
    {
        public static IServiceCollection AddLogSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Log provider: https://kisslog.net/
            services.AddScoped<ILogger>((context) =>
            {
                return Logger.Factory.Get();
            });

            return services;
        }

        public static IApplicationBuilder UseLogSetup(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            // make sure it is added after app.UseStaticFiles() and app.UseSession(), and before app.UseMvc()
            app.UseKissLogMiddleware(options =>
            {
                options.Listeners.Add(new KissLogApiListener(new KissLog.Apis.v1.Auth.Application(
                    configuration["KissLog.OrganizationId"],
                    configuration["KissLog.ApplicationId"])
                ));
            });

            return app;
        }
    }
}
