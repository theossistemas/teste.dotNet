using LibraryStore.Core.Business;
using LibraryStore.Core.IoC.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryStore.Core.IoC.Injectors
{
    public class BusinessInjector : IInjector
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAccountBusiness, AccountBusiness>();
            services.AddScoped<IBookBusiness, BookBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();
        }
    }
}