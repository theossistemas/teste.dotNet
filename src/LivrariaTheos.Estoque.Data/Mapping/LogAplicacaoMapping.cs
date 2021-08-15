using LivrariaTheos.Estoque.Domain.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivrariaTheos.Estoque.Data.Mapping
{
    public class LogAplicacaoMapping : IEntityTypeConfiguration<LogAplicacao>
    {
        public void Configure(EntityTypeBuilder<LogAplicacao> builder)
        {
            builder.ToTable("LogAplicacao");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Metodo)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(t => t.MensagemErro).HasColumnType("varchar(max)"); 
            builder.Property(t => t.StackTrace).HasColumnType("varchar(max)");


            builder.Ignore(t => t.CascadeMode);
            builder.Ignore(t => t.DataAlteracao);
            builder.Ignore(t => t.UsuarioAlteracao);
        }
    }
}
