using Microsoft.EntityFrameworkCore;
using LivrariaWeb.Domain.Model;

namespace LivrariaWeb.Infra.Map
{
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoa");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).HasColumnName("nome_pessoa").IsRequired();
            builder.Property(x => x.Email).HasColumnName("email").IsRequired();
            builder.Property(x => x.Senha).HasColumnName("senha").IsRequired();
        }
    }
}
