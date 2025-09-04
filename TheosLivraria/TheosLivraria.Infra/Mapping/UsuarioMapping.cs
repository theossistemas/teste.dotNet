using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Infra.Mapping.Base;

namespace TheosLivraria.Infra.Mapping
{
    public class UsuarioMapping : EntidadeBaseMapping<int, Usuario>
    {
        public override void Map(EntityTypeBuilder<Usuario> builder)
        {
            base.Map(builder);
            builder.ToTable("usuarios");

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasColumnName("nome")
                .HasMaxLength(200);

            builder.Property(u => u.Documento)
                .IsRequired()
                .HasColumnName("documento")
                .HasMaxLength(14);
            builder.HasIndex(u => u.Documento).IsUnique();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(200);
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Senha)
                .IsRequired()
                .HasColumnName("senha")
                .HasMaxLength(64);

            builder.Property(u => u.Telefone)
                .IsRequired()
                .HasColumnName("telefone")
                .HasMaxLength(15);

            builder.Property(u => u.DataNascimento)
                .IsRequired()
                .HasColumnName("data_nascimento");

            builder.Property(u => u.Perfil)
                .IsRequired()
                .HasColumnName("perfil")
                .HasConversion<int>();


        }
    }
}
