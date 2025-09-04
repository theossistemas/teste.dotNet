using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Domain.Entidades;

namespace TheosLivraria.Infra.Mapping.Base
{
    public abstract class EntidadeBaseMapping<TId, TEntity> where TEntity : EntidadeBase<TId> where TId : struct
    {
        public virtual void Map(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Uuid)
                .IsRequired()
                .HasColumnName("uuid")
                .HasMaxLength(30);

            builder.Property(e => e.DataCriacao)
                .IsRequired()
                .HasColumnName("data_criacao");

            builder.Property(e => e.DataAtualizacao)
                .IsRequired(false)
                .HasColumnName("data_atualizacao");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasColumnName("ativo");
        }
    }
}
