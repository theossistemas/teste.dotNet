using JeffersonMello.Livraria.Model.Abstract;
using Microsoft.EntityFrameworkCore;

namespace JeffersonMello.Livraria.Data.Abstract
{
    public abstract class EntityConfigBase<TEntity> : ConfigBase<TEntity>
        where TEntity : EntityBase
    {
        protected override void ConfigureFieldsTable()
        {
            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName("guid");

            builder.Property(p => p.DataCriacao)
                .HasColumnName("datacriacao");

            builder.Property(p => p.DataAlteracao)
                .HasColumnName("dataalteracao");
        }

        protected override void ConfigurePK()
        {
            builder.HasKey(pk => pk.Id);
        }
    }
}