using Microsoft.Extensions.DependencyInjection;
using Theos.Library.Core.Business;
using Theos.Library.Core.Business.Interface;
using Theos.Library.Core.Data.Repository;
using Theos.Library.Core.Data.Repository.Interface;

namespace Theos.Library.Api.Injection
{
    public static class Factory
    {
        public static void Build(IServiceCollection services)
        {
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IBookRepository, BookRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

            Core.Business.Injection.Factory.SetServices(services.BuildServiceProvider());
        }

        public static TS GetInstance<T, TS>()
        {
            return Core.Business.Injection.Factory.GetInstance<T, TS>();
        }
    }
}