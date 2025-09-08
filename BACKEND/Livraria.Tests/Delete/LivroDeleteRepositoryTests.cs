using Livraria.Infrastructure.Repositories;
using Livraria.Domain;
using Livraria.Infrastructure;
using Livraria.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Livraria.Tests.Delete
{
    public class LivroDeleteRepositoryTests
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
        public async Task DeleteAsync_DeveRetornarTrue_QuandoExclusaoBemSucedida()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroDeleteRepository>>();
            var repo = new LivroDeleteRepository(db, logger.Object);

            var livro = GetLivro();
            db.Livros.Add(livro);
            await db.SaveChangesAsync();

            var result = await repo.DeleteAsync(livro.Id);

            Assert.True(result);
            var livroDb = await db.Livros.FindAsync(livro.Id);
            Assert.False(livroDb.Ativo);
            Assert.NotNull(livroDb.ExcluidoEm);
        }

        [Fact]
        public async Task DeleteAsync_DeveLancarNaoEncontradoException_QuandoLivroNaoExiste()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroDeleteRepository>>();
            var repo = new LivroDeleteRepository(db, logger.Object);

            await Assert.ThrowsAsync<NaoEncontradoException>(() => repo.DeleteAsync(999));
        }

        [Fact]
        public async Task DeleteAsync_DeveLancarOperacaoNaoPermitidaException_QuandoLivroJaInativo()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroDeleteRepository>>();
            var repo = new LivroDeleteRepository(db, logger.Object);

            var livro = GetLivro(ativo: false);
            db.Livros.Add(livro);
            await db.SaveChangesAsync();

            await Assert.ThrowsAsync<OperacaoNaoPermitidaException>(() => repo.DeleteAsync(livro.Id));
        }

        [Fact]
        public async Task DeleteAsync_DeveLancarConflitoDeDadosException_QuandoDbUpdateException()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroDeleteRepository>>();
            var repo = new LivroDeleteRepository(db, logger.Object);

            var livro = GetLivro();
            db.Livros.Add(livro);
            await db.SaveChangesAsync();

            db.Dispose(); // Simula erro de banco

            await Assert.ThrowsAsync<ConflitoDeDadosException>(() => repo.DeleteAsync(livro.Id));
        }
    }
}
