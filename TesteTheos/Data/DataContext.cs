using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TesteTheos.Data
{
    public class DataContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Livro>().HasQueryFilter(l => !l.Excluido);

            modelBuilder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid> { Id = new Guid("{12B6F91A-4A12-4C0F-BC25-CA791CD4C20D}"), Name = "Administrador", NormalizedName = "Administrador".ToUpper() });

            var hasher = new PasswordHasher<Usuario>();
            var admin = new Usuario { Id = new Guid("{EA5966AB-EAF0-4A04-BEDA-3C926BB7B9BD}"), UserName = "Administrador", NormalizedUserName = "Administrador".ToUpper(), PasswordHash = hasher.HashPassword(null, "123456") };
            modelBuilder.Entity<Usuario>().HasData(admin);

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = new Guid("{12B6F91A-4A12-4C0F-BC25-CA791CD4C20D}"),
                    UserId = new Guid("{EA5966AB-EAF0-4A04-BEDA-3C926BB7B9BD}")
                }
            );

            modelBuilder.Entity<Livro>().HasData(
                new Livro { Id = new Guid("{0C4440E5-F9CF-48D1-9964-1A2E18F68AEB}"), Nome = "O Senhor dos Anéis: A Sociedade do Anel", Autor = "J.R.R. Tolkien", Sinopse = "Sinopse A Sociedade do Anel", DataCriacao = DateTime.Now, CriadoPorId = admin.Id },
                new Livro { Id = new Guid("{511862F2-C654-4FEA-9233-CDC8B3956392}"), Nome = "O Senhor dos Anéis: As duas torres", Autor = "J.R.R. Tolkien", Sinopse = "Sinopse As duas torres", DataCriacao = DateTime.Now, CriadoPorId = admin.Id },
                new Livro { Id = new Guid("{D5DF707F-16AB-48B2-8601-B7E49305C6F3}"), Nome = "O Senhor dos Anéis: O retorno do rei", Autor = "J.R.R. Tolkien", Sinopse = "Sinopse O retorno do rei", DataCriacao = DateTime.Now, CriadoPorId = admin.Id }
            );
        }

        public override EntityEntry<TEntity> Remove<TEntity>([NotNull] TEntity entity)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                return SoftDelete(entity);
            }

            return base.Remove(entity);
        }

        protected EntityEntry<TEntity> SoftDelete<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            ISoftDelete convertedEntity = (ISoftDelete)entity;

            var entry = Entry(convertedEntity);
            if (entry.State == EntityState.Detached)
            {
                Attach(convertedEntity);
            }

            convertedEntity.Excluido = true;
            convertedEntity.DataExclusao = DateTime.Now;
            entry.Property(q => q.ExcluidoPorId).IsModified = true;

            return Entry(entity);
        }


        public DbSet<Livro> Livros { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
