using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Domain.Entidades;

namespace TheosLivraria.Tests.Domain
{
    public class LivroTests
    {
        [Fact]
        public void Criar_Livro_Valido()
        {
            var livro = new Livro(
                titulo: "Senhor dos Aneis",
                autor: "JK TOLKIEN",
                isbn: "1234567890123",
                dataPublicacao: new DateTime(1927, 1, 1),
                preco: 50.99m,
                estoque: 10
            );

            Assert.Equal("Senhor dos Aneis", livro.Titulo);
            Assert.Equal("JK TOLKIEN", livro.Autor);
            Assert.Equal("1234567890123", livro.Isbn);
            Assert.Equal(new DateTime(1927, 1, 1), livro.DataPublicacao);
            Assert.Equal(50.99m, livro.Preco);
            Assert.Equal(10, livro.Estoque);
        }

        [Fact]
        public void Alterar_Dados_Livro()
        {
            var livro = new Livro("Senhor dos Aneis", "JK TOLKIEN", "1234567890123", new DateTime(1927, 1, 1), 10, 5);

            livro.AlterarDados("Senhor dos Aneis - Sociedade do Anel", "J K TOLKIEN", "9876543210123", new DateTime(1929, 10, 2), 109.10m, 21);

            Assert.Equal("Senhor dos Aneis - Sociedade do Anel", livro.Titulo);
            Assert.Equal("J K TOLKIEN", livro.Autor);
            Assert.Equal("9876543210123", livro.Isbn);
            Assert.Equal(new DateTime(1929, 10, 2), livro.DataPublicacao);
            Assert.Equal(109.10m, livro.Preco);
            Assert.Equal(21, livro.Estoque);
            Assert.NotNull(livro.DataAtualizacao);
        }

        [Theory]
        [InlineData("", "Autor", "1234567890123", "Título deve ter pelo menos 2 caracteres.")]
        [InlineData("Li", "", "1234567890123", "Autor deve ter pelo menos 2 caracteres.")]
        [InlineData("Livro", "Autor", "123456789012", "ISBN deve ter exatamente 13 dígitos numéricos.")]
        [InlineData("Livro", "Autor", "123456789012a", "ISBN deve ter exatamente 13 dígitos numéricos.")]
        [InlineData("Livro", "Autor", "1234567890123", "Data de publicação não pode ser no futuro.", 2100, 1, 1)]
        [InlineData("Livro", "Autor", "1234567890123", "Preço não pode ser negativo.", 2020, 1, 1, -1)]
        [InlineData("Livro", "Autor", "1234567890123", "Estoque não pode ser negativo.", 2020, 1, 1, 10, -5)]
        public void NAO_Criar_Livro_Invalido(
            string titulo, string autor, string isbn, string mensagemEsperada,
            int ano = 2020, int mes = 1, int dia = 1, decimal preco = 10, int estoque = 5)
        {
            var dataPublicacao = new DateTime(ano, mes, dia);

            var ex = Assert.Throws<ArgumentException>(() =>
                new Livro(titulo, autor, isbn, dataPublicacao, preco, estoque)
            );
            Assert.Contains(mensagemEsperada, ex.Message);
        }
    }
}
