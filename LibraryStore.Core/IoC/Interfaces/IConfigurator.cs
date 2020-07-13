using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace LibraryStore.Core.IoC.Interfaces
{
    public interface IConfigurator
    {
        public void ConfigureAppBuilder(IApplicationBuilder app, IConfiguration configuration);
    }
}