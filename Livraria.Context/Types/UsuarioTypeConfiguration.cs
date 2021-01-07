using Livraria.Domain.Usuarios;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Context.Types
{
    public class UsuarioTypeConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Login).IsUnique();

            builder.HasOne(u => u.Pessoa).WithMany().HasForeignKey(u => u.IdPessoa);

            builder.HasData
                (
                    new Usuario
                    {
                        Id = 1,
                        IdPessoa = 1,
                        Email = "teste@123",
                        Login = "admin",
                        Senha = "123",
                        Permissao = Permissao.Administrador
                    }
                );
        }
    }
}
