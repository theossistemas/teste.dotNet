using LibraryStore.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryStore.Setup
{
    public class BaseSetup
    {
        protected SwaggerConfiguration LoadSwaggerConfiguration(IConfiguration configuration)
        {
            var cfg = new SwaggerConfiguration();

            configuration.GetSection(nameof(SwaggerConfiguration)).Bind(cfg);

            return cfg;
        }
    }
}