using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using teste.dotNet.Controllers;
using teste.dotNet.Models;
using Xunit;

namespace teste.dotNet.Tests
{
    public class LivroTest
    {
        public LivroTest()
        {
            InitContext();
        }

        private Context _context; 

        private void InitContext()
        {
            var builder = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase();

            var context = new Context(builder.Options);
            var livros = Enumerable.Range(1, 10)
                .Select(i => new Livro {Id = i, Nome = $"Livro {i}", Preco = 10 + i, Quantidade = i});
            context.Livros.AddRange(livros);
            int changed = context.SaveChanges();
            _context = context;
        }

        [Fact]
        public void TestGetByIdLivro()
        {
            string expectedNome = "Livro 2";
            var controller = new LivrosController(_context);
            var resultado = controller.GetById(2).Result;
            Assert.Equal(expectedNome, resultado.Nome);
        }

        [Fact]
        public void TestGetLivro()
        {
            var controller = new LivrosController(_context);
            var resultado = controller.Get();
            Assert.Equal(10, resultado.Count());
        }

        [Fact]
        public void TestPutLivro()
        {
            var controller = new LivrosController(_context);
            var resultado = controller.Get();
            var livro = resultado.ElementAt(9);
            var novoNome = "Nome novo do livro";
            livro.Nome = novoNome;
            controller.Put(livro.Id, livro).GetAwaiter();
            var resultadoPorId = controller.GetById(livro.Id).Result;
            Assert.Equal(novoNome, resultadoPorId.Nome);
        }

        [Fact]
        public void TestPostLivro()
        {
            var controller = new LivrosController(_context);
            var livro = new Livro {Id = 999, Nome = "Livro Test", Quantidade = 9, Preco = 10};
            controller.Post(livro).GetAwaiter();
            var resultado = controller.GetById(999).Result;
            Assert.Equal(livro.Nome, resultado.Nome);
        }

        [Fact]
        public void TestDeleteLivro()
        {
            var controller = new LivrosController(_context);
            var deleted = controller.Delete(1).GetAwaiter();
            Assert.True(deleted.IsCompleted);
        }
    }
}
