using ProjetoLivraria.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoLivraria.Data.EntityConfiguration
{
    public class LivrariaConfiguration : EntityTypeConfiguration<Livro>
    {
        public LivrariaConfiguration()
        {
            ToTable("Livros");

            HasKey(l => l.IdLivro);
            Property(t => t.IdLivro).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(l => l.Nome).IsRequired().HasMaxLength(50);

            Property(l => l.Editora).IsRequired().HasMaxLength(50);

            Property(l => l.Autor).IsRequired().HasMaxLength(200);

            Property(l => l.Categoria).IsRequired().HasMaxLength(200);

           

        }
    }
}
