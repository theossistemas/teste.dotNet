using Livraria.Infrastructure.Repositories;
using Livraria.Domain;
using Livraria.Infrastructure;
using Livraria.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Livraria.Tests.Update
{
    public class LivroUpdateRepositoryTests
    {
        private LivrariaDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<LivrariaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new LivrariaDbContext(options);
        }

        private Livro GetLivro(int id = 1, bool ativo = true)
        {
            return new Livro(id, "Título", "Autor", "1234567890", 2020, 10, 99.99m)
            {
                Ativo = ativo
            };
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarLivro_QuandoLivroExisteEAtivo()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroUpdateRepository>>();
            var repo = new LivroUpdateRepository(db, logger.Object);

            var livro = GetLivro();
            db.Livros.Add(livro);
            await db.SaveChangesAsync();

            livro.Titulo = "Novo Título";
            var result = await repo.UpdateAsync(livro);

            Assert.NotNull(result);
            Assert.Equal("Novo Título", result.Titulo);
        }

        [Fact]
        public async Task UpdateAsync_DeveLancarNaoEncontradoException_QuandoLivroNaoExiste()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroUpdateRepository>>();
            var repo = new LivroUpdateRepository(db, logger.Object);

            var livro = GetLivro(id: 999);

            await Assert.ThrowsAsync<NaoEncontradoException>(() => repo.UpdateAsync(livro));
        }

        [Fact]
        public async Task UpdateAsync_DeveLancarOperacaoNaoPermitidaException_QuandoLivroInativo()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroUpdateRepository>>();
            var repo = new LivroUpdateRepository(db, logger.Object);

            var livro = GetLivro(ativo: false);
            db.Livros.Add(livro);
            await db.SaveChangesAsync();

            await Assert.ThrowsAsync<OperacaoNaoPermitidaException>(() => repo.UpdateAsync(livro));
        }

        [Fact]
        public async Task UpdateAsync_DeveLancarConflitoDeDadosException_QuandoDbUpdateException()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroUpdateRepository>>();
            var repo = new LivroUpdateRepository(db, logger.Object);

            var livro = GetLivro();
            db.Livros.Add(livro);
            await db.SaveChangesAsync();

            db.Dispose(); // simula erro de banco

            await Assert.ThrowsAsync<ConflitoDeDadosException>(() => repo.UpdateAsync(livro));
        }
    }
}
