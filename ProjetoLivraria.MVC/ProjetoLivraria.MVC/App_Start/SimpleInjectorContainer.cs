using ProjetoLivraria.Application;
using ProjetoLivraria.Application.Interface;

using System.Web.Http;
using SimpleInjector;
using ProjetoLivraria.Domain.Interfaces.Services;
using ProjetoLivraria.Domain.Services;
using ProjetoLivraria.Domain.Interfaces.Repositories;
using ProjetoLivraria.Data.Repositories;
using System.Reflection;
using SimpleInjector.Integration.Web;
using System.Web.Mvc;
using SimpleInjector.Integration.Web.Mvc;

namespace ProjetoLivraria.MVC.App_Start
{
    public static class SimpleInjectorContainer
    {
        public static Container RegisterServices()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<ILivroApplication, LivroApplication>(Lifestyle.Singleton);
            container.Register<ILivroService, LivroService>(Lifestyle.Singleton);
            container.Register<ILivroRepository, LivroRepository>(Lifestyle.Singleton);

            container.Register(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            container.Register(typeof(IBaseService<>), typeof(BaseService<>));

           
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration); //web api        
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            container.Verify();

            return container;
        }
    }
}