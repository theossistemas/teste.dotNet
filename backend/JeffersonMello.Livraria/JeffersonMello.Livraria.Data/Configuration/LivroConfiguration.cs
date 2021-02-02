using JeffersonMello.Livraria.Data.Abstract;
using JeffersonMello.Livraria.Model.Cadastro.Item;
using Microsoft.EntityFrameworkCore;

namespace JeffersonMello.Livraria.Data.Configuration
{
    public class LivroConfiguration : EntityConfigBase<Livro>
    {
        #region Protected Methods

        protected override void ConfigureFieldsTable()
        {
            base.ConfigureFieldsTable();

            builder.Property(p => p.Titulo)
               .IsRequired()
               .HasMaxLength(100)
              .HasColumnName("titulo");

            builder.Property(p => p.Descricao)
              .IsRequired()
             .HasColumnName("descricao");

            builder.Property(p => p.Autor)
              .IsRequired()
              .HasMaxLength(150)
             .HasColumnName("autor");

            builder.Property(p => p.Marca)
              .HasMaxLength(100)
             .HasColumnName("marca");

            builder.Property(p => p.DataLancamento)
              .IsRequired()
             .HasColumnName("datalancamento");

            builder.Property(p => p.CategoriaId)
              .IsRequired()
             .HasColumnName("guid_categoria");
        }

        protected override void ConfigureFK()
        {
            builder.HasOne(p => p.Categoria)
               .WithMany()
               .HasForeignKey(fk => fk.CategoriaId);
        }

        protected override void ConfigureTableName()
        {
            builder.ToTable("cad_livro");
        }

        #endregion Protected Methods
    }
}