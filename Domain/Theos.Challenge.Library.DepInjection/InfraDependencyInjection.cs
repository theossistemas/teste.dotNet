using Microsoft.Extensions.DependencyInjection;
using Theos.Challenge.Library.Model.Contracts.Infra;
using Theos.Challenge.Library.Orm.Dapper.Repository;

namespace Theos.Challenge.Library.DepInjection
{
    public static class InfraDependencyInjection
    {
        public static IServiceCollection AddInfraDI (this IServiceCollection services)
        {             
            services.AddScoped<IBookRepository, BookDapperRepository>();                                  
            return services;
        }
    }
}