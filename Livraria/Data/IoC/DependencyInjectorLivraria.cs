using Livraria.Application.Apps;
using Livraria.Application.Interfaces;
using Livraria.Data.Repository;
using Livraria.Inferfaces.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Data.IoC
{
    public class DependencyInjectorLivraria
    {
        public static void Registrar(IServiceCollection svcCollection)
        {
            svcCollection.AddScoped<ILivroRepository, LivroRepository>();

            svcCollection.AddScoped<ILivroAppService, LivroAppService>();
        }
    }
}
