using Livraria.Command;
using Livraria.Command.Notifications;
using Livraria.Data.Dapper.Repositories;
using Livraria.Data.EFCore.Context;
using Livraria.Data.EFCore.Repositories;
using Livraria.Data.EFCore.UoW;
using Livraria.Domain.Interface;
using Livraria.Domain.Interface.QueryRepositories;
using Livraria.Domain.Interface.Repositories;
using Livraria.Service.Classes;
using Livraria.Service.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace Livraria.IoC
{
    public class DependencyInjector
    {
        public static void Inject(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LivrariaContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();

            services.AddScoped<IRequestHandler<IncluirAutorCommand>, IncluirAutorHandler>();
            services.AddScoped<IRequestHandler<IncluirEditoraCommand>, IncluirEditoraHandler>();
            services.AddScoped<IRequestHandler<IncluirLivroCommand>, IncluirLivroHandler>();
            services.AddScoped<IRequestHandler<AlterarLivroCommand>, AlterarLivroHandler>();
            services.AddScoped<IRequestHandler<ExcluirLivroCommand>, ExcluirLivroHandler>();

            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<IEditoraRepository, EditoraRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<ILivroQueryRepository, LivroQueryRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<LivrariaContext>();
            services.AddScoped(x => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
