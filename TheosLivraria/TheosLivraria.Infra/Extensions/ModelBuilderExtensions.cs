using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Infra.Mapping.Base;

namespace TheosLivraria.Infra.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfigurationMapping<TId, TEntity>(this ModelBuilder modelBuilder, EntidadeBaseMapping<TId, TEntity> configuration)
            where TEntity : EntidadeBase<TId> where TId : struct
        {
             configuration.Map(modelBuilder.Entity<TEntity>());
        }
    }
}
