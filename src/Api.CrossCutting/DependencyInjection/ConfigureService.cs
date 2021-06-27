using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Book;
using Api.Domain.Interfaces.User;
using Api.Service.Services;
using Api.Service.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Repository
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IBookService, BookService>();
            serviceCollection.AddTransient<ILoginService, LoginService>();
            serviceCollection.AddTransient<ILogErrorService, LogErrorService>();
            serviceCollection.AddTransient<IAdminService, AdminService>();
        }
    }
}
