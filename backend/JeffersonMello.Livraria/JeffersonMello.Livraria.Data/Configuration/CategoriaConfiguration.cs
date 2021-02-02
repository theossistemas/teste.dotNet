using JeffersonMello.Livraria.Data.Abstract;
using JeffersonMello.Livraria.Model.Cadastro.Item;
using Microsoft.EntityFrameworkCore;

namespace JeffersonMello.Livraria.Data.Configuration
{
    public class CategoriaConfiguration : EntityConfigBase<Categoria>
    {
        #region Protected Methods

        protected override void ConfigureFieldsTable()
        {
            base.ConfigureFieldsTable();

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(250)
               .HasColumnName("descricao");
        }

        protected override void ConfigureFK()
        {
        }


        protected override void ConfigureTableName()
        {
            builder.ToTable("cad_categoria");
        }

        #endregion Protected Methods
    }
}