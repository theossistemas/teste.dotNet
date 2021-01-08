using System.Collections.Generic;
using BooksApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Data
{
    public class DataContext : DbContext
    {
      
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(p =>
            {
                p.ToTable("Usuarios");
                p.HasKey(a => a.Id);
                p.Property(a => a.Nome).HasColumnType("VARCHAR(80)").IsRequired();
                p.Property(a => a.NomeUsuario).HasColumnType("VARCHAR(30)").IsRequired();
                p.Property(a => a.Email).HasColumnType("VARCHAR(50)").IsRequired();
                p.Property(a => a.Role).HasColumnType("VARCHAR(50)");
                p.Property(a => a.Senha).HasColumnType("VARCHAR(50)").IsRequired();
                p.HasIndex(a => a.Email).IsUnique().HasName("IDX_EMAIL");
            });
            
            modelBuilder.Entity<Livro>(p =>
            {
                p.ToTable("Livros");
                p.HasKey(c => c.Id);
                p.Property(c => c.Titulo).HasColumnType("varchar(80)").IsRequired();
                p.Property(c => c.TotalPagina).HasColumnType("int").IsRequired();
                p.Property(c => c.Valor).HasColumnType("decimal(10,2)").IsRequired();
                p.Property(c => c.ValorPromocao).HasColumnType("decimal(10,2)").IsRequired();
                p.Property(c => c.Promocao).HasColumnType("bit").IsRequired();
                p.Property(c => c.Isbn).HasColumnType("VARCHAR(20)").IsRequired();
                p.Property(c => c.Autor).HasColumnType("VARCHAR(80)").IsRequired();
                p.Property(c => c.ImagemUrl).HasMaxLength(100);
                p.HasIndex(c => c.Isbn).IsUnique().HasName("IDX_ISBN");
                p.HasIndex(c => c.Titulo).HasName("IDX_TITLE");
            });
            //
           
            //has data
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario()
                {
                    Id = 1,
                    Nome = "Administrador",
                    NomeUsuario = "admin",
                    Senha = "123456789",
                    Email = "admin@email.com",
                    Role = "admin"
                }
            );
            modelBuilder.Entity<Livro>().HasData(
                new List<Livro>()
                {
                    new Livro()
                    {
                        Id = 1,
                        Titulo = "Net Core 5.0",
                        Isbn = "123456785",
                        Autor = "PEDRO SILVA",
                        TotalPagina = 225,
                        Promocao = false,
                        Valor = 150,
                        ValorPromocao = 0,
                        Resumo = "mauris cursus mattis molestie a iaculis at erat pellentesque adipiscing commodo elit at imperdiet dui accumsan sit amet nulla facilisi morbi tempus iaculis urna id volutpat lacus laoreet non curabitur gravida arcu ac tortor dignissim convallis aenean et tortor at risus viverra adipiscing at in tellus integer feugiat scelerisque varius",
                        ImagemUrl = ""
                        
                    },
                    new Livro()
                    {
                        Id = 2,
                        Titulo = "React ",
                        Isbn = "123456789101579",
                        Autor = "JOSEFA ANTONIA",
                        TotalPagina = 250,
                        Promocao = false,
                        Valor = 80,
                        ValorPromocao = 0,
                        Resumo = "mauris cursus mattis molestie a iaculis at erat pellentesque adipiscing commodo elit at imperdiet dui accumsan sit amet nulla facilisi morbi tempus iaculis urna id volutpat lacus laoreet non curabitur gravida arcu ac tortor dignissim convallis aenean et tortor at risus viverra adipiscing at in tellus integer feugiat scelerisque varius",
                        ImagemUrl = ""
                    },
                    new Livro()
                    {
                        Id = 3,
                        Titulo = "Angustia",
                        Isbn = "753214789",
                        Autor = "GRACILIANO RAMOS",
                        TotalPagina = 157,
                        Promocao = true,
                        Valor = 35,
                        ValorPromocao = 19,
                        Resumo = "",
                        ImagemUrl = ""
                    },
                    
                });


        }
    }
}