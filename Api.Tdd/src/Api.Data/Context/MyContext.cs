using Api.Domain.Entities;
using Base.Domain.Entities.Cadastros.Base;
using Data.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class MyContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        #region Dbset Entidades Base
        public DbSet<ApplicationUser> Usuario { get; set; }
        public DbSet<Livro> Livro { get; set; }
        #endregion


        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Configuracao Entidades Base
            builder.Entity<Livro>(new LivroMap().Configure);
           
            #endregion           
            #region Configuracao Identity 
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Usuarios");                
                entity.Property(c => c.PerfilId).HasColumnName("IdPerfil");
                entity.Property(c => c.AccessFailedCount).HasColumnName("AcessoFalhas");
                entity.Property(c => c.ConcurrencyStamp).HasColumnName("Concorrencia");
                entity.Property(c => c.EmailConfirmed).HasColumnName("EmailConfirmado");
                entity.Property(c => c.LockoutEnabled).HasColumnName("BloqueioAtivo");
                entity.Property(c => c.LockoutEnd).HasColumnName("FimBloqueio");
                entity.Property(c => c.NormalizedEmail).HasColumnName("EmailNormalizado");
                entity.Property(c => c.NormalizedUserName).HasColumnName("LoginNormalizado");
                entity.Property(c => c.PasswordHash).HasColumnName("SenhaHash");
                entity.Property(c => c.PhoneNumber).HasColumnName("TelefoneNumero");
                entity.Property(c => c.PhoneNumberConfirmed).HasColumnName("TelefoneConfirmado");
                entity.Property(c => c.SecurityStamp).HasColumnName("Seguranca");
                entity.Property(c => c.TwoFactorEnabled).HasColumnName("VerificaoDoidPassos");
                entity.Property(c => c.UserName).HasColumnName("Login");
                entity.Property(c => c.Id).HasColumnType("int");
            });

            builder.Entity<IdentityRole<int>>(entity =>
            {
                entity.ToTable(name: "Politicas");
                entity.Property(c => c.Name).HasColumnName("Nome");
                entity.Property(c => c.NormalizedName).HasColumnName("NomeNormalizado");
                entity.Property(c => c.ConcurrencyStamp).HasColumnName("Concorrencia");
            });
            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UsuariosPoliticas");
                entity.Property(c => c.UserId).HasColumnName("IdUsuario");
                entity.Property(c => c.RoleId).HasColumnName("IdPolitica");
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UsuariosPapeis");
                entity.Property(c => c.ClaimType).HasColumnName("Tipo");
                entity.Property(c => c.ClaimValue).HasColumnName("Valor");
                entity.Property(c => c.UserId).HasColumnName("IdUsuario");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UsuariosLogins");
                entity.Property(c => c.LoginProvider).HasColumnName("LoginProvavel");
                entity.Property(c => c.ProviderDisplayName).HasColumnName("NomeProvavel");
                entity.Property(c => c.ProviderKey).HasColumnName("ChaveProvavel");
                entity.Property(c => c.UserId).HasColumnName("IdUsuario");
            });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("PoliticasClaims");
                entity.Property(c => c.ClaimType).HasColumnName("Tipo");
                entity.Property(c => c.ClaimValue).HasColumnName("Valor");
                entity.Property(c => c.RoleId).HasColumnName("IdPolitica");
            });

            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UsuariosTokens");
                entity.Property(c => c.LoginProvider).HasColumnName("LoginProvavel");
                entity.Property(c => c.Name).HasColumnName("Nome");
                entity.Property(c => c.UserId).HasColumnName("IdUsuario");
                entity.Property(c => c.Value).HasColumnName("Valor");
            });

            #endregion
           
        }
    }
}
