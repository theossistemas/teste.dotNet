using Domain.Interface;
using Microsoft.Extensions.DependencyInjection;
using Service.ApplicationService;

namespace Infra.Config
{
    public static class ConfigApplicationService
    {
        public static void ConfigureApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
