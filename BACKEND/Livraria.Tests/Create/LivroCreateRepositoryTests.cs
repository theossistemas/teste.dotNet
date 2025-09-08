using Livraria.Infrastructure.Repositories;
using Livraria.Domain;
using Livraria.Infrastructure;
using Livraria.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;

namespace Livraria.Tests.Create
{
    public class LivroCreateRepositoryTests
    {
        private LivrariaDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<LivrariaDbContext>()  
                .UseInMemoryDatabase(databaseName: "LivrariaTestDb")
                .Options;
            return new LivrariaDbContext(options);
        }

        private Livro GetLivro(string isbn = "1234567890", string titulo = "Título", string autor = "Autor")
        {
            return new Livro(0, titulo, autor, isbn, 2020, 10, 99.99m) { Ativo = true };
        }

        [Fact]
        public async Task AddAsync_DeveCriarLivro_QuandoDadosValidos()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroCreateRepository>>();
            var repo = new LivroCreateRepository(db, logger.Object);

            var livro = GetLivro();

            var result = await repo.AddAsync(livro);

            Assert.NotNull(result);
            Assert.Equal("Título", result.Titulo);
            Assert.True(db.Livros.AnyAsync(l => l.ISBN == livro.ISBN && l.Ativo).Result);
        }

        [Fact]
        public async Task AddAsync_DeveLancarOperacaoNaoPermitidaException_QuandoTituloOuAutorVazio()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroCreateRepository>>();
            var repo = new LivroCreateRepository(db, logger.Object);

            var livro = GetLivro(titulo: "", autor: "");

            await Assert.ThrowsAsync<OperacaoNaoPermitidaException>(() => repo.AddAsync(livro));
        }

        [Fact]
        public async Task AddAsync_DeveLancarConflitoDeDadosException_QuandoISBNDuplicado()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroCreateRepository>>();
            var repo = new LivroCreateRepository(db, logger.Object);

            var livro1 = GetLivro();
            var livro2 = GetLivro();

            await repo.AddAsync(livro1);
            await Assert.ThrowsAsync<ConflitoDeDadosException>(() => repo.AddAsync(livro2));
        }

        [Fact]
        public async Task AddAsync_DeveLancarConflitoDeDadosException_QuandoDbUpdateException()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroCreateRepository>>();
            var repo = new LivroCreateRepository(db, logger.Object);

            var livro = GetLivro();

            // Simula erro de banco removendo o DbSet
            db.Dispose();

            await Assert.ThrowsAsync<ConflitoDeDadosException>(() => repo.AddAsync(livro));
        }
    }
}
