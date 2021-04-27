using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Livraria.Infra.Data.Mapping
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder){
            builder.ToTable("Livro");
            builder.HasKey(p => p.Id);

             builder.Property(p => p.Id)
            .UseIdentityColumn()
            .HasColumnName("Id");
            
            

            builder.Property(p => p.Autor)
            .IsRequired()            
            .HasColumnName("Autor")
            .HasColumnType("varchar(50)");

            builder.Property(p => p.Nome)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("varchar(50)");
            
            builder.Property(p => p.Editora)
            .IsRequired()
            .HasColumnName("Editora")
            .HasColumnType("varchar(50)");

            builder.Property(p => p.Sinopse)            
            .HasColumnName("Sinopse")
            .HasColumnType("varchar(1000)");

            builder.Property(p => p.Genero)
            .IsRequired()
            .HasColumnName("Genero")
            .HasColumnType("varchar(20)");
            
            builder.Property(p => p.DataCriacao)            
            .HasColumnName("DataCriacao")
            .HasColumnType("datetime");
            
            builder.Property(p => p.DataAtualizacao)            
            .HasColumnName("DataAtualizacao")
            .HasColumnType("datetime");


        }
        
    }
}