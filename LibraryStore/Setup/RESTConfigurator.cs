using LibraryStore.Core.IoC.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace LibraryStore.Setup
{
    public class RESTConfigurator : BaseSetup, IConfigurator
    {
        public void ConfigureAppBuilder(IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerConfiguration = LoadSwaggerConfiguration(configuration);

            app.UseSwagger(options => { options.RouteTemplate = swaggerConfiguration.JsonRoute; });
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint(
                    swaggerConfiguration.UIEndpoint,
                    swaggerConfiguration.Description
                );
                opt.RoutePrefix = swaggerConfiguration.RoutePrefix;
            });
        }
    }
}