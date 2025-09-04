using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Infra.Mapping.Base;

namespace TheosLivraria.Infra.Mapping
{
    public class LogMapping : EntidadeBaseMapping<int, Log>
    {
        public override void Map(EntityTypeBuilder<Log> builder)
        {
            base.Map(builder);
            builder.ToTable("logs");

            builder.Property(l => l.Tipo)
                .IsRequired()
                .HasColumnName("tipo")
                .HasConversion<int>();

            builder.Property(l => l.Origem)
                .IsRequired()
                .HasColumnName("origem")
                .HasMaxLength(200);

            builder.Property(l => l.DadosJson)
                .HasColumnName("dados_json")
                .HasColumnType("text");

            builder.Property(l => l.UsuarioId)
                .HasColumnName("usuario_id");

            builder.HasOne(l => l.Usuario)
                .WithMany()
                .HasForeignKey(l => l.UsuarioId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
