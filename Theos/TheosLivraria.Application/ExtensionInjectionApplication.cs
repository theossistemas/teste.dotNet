
using Microsoft.Extensions.DependencyInjection;
using TheosLivraria.Application.UseCases.Book.Create;
using TheosLivraria.Application.UseCases.Book.Delete;
using TheosLivraria.Application.UseCases.Book.GetAll;
using TheosLivraria.Application.UseCases.Book.GetAllPaged;
using TheosLivraria.Application.UseCases.Book.Update;
using TheosLivraria.Application.UseCases.User.CreateAdmin;
using TheosLivraria.Application.UseCases.User.GetLogin;
using TheosLivraria.Domain.Interfaces.Notification;
using TheosLivraria.Domain.Interfaces.UseCases;
using TheosLivraria.Domain.Models.Notification;

namespace TheosLivraria.Application
{
    public static class ExtensionInjectionApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region UseCases

            #region User
            services.AddScoped<ICreateAdminUserCase, CreateAdminUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
            #endregion


            #region Book
            services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
            services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();
            services.AddScoped<ICreateBookUseCase, CreateBookUseCase>();
            services.AddScoped<IGetAllPagedUseCase, GetAllPagedUseCase>();
            services.AddScoped<IGetAllUseCase, GetAllUseCase>();
            #endregion

            #endregion

            #region Domain
            services.AddScoped<IDomainNotification, DomainNotification>();
            #endregion

            return services;
        }
    }
}
