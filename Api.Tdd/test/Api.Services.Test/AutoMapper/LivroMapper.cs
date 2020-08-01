using Api.Domain.Entities;
using Api.Services.Test.Base;
using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Api.Services.Test.AutoMapper
{
    public class LivroMapper : BaseTesteService
    {
        [Fact(DisplayName = "É possivel mappear Dto")]
        public void E_Possivel_Mappear_Dto()
        {
            var entidade = NovoLivro();

            var livroDto = Mapper.Map<LivroDto>(entidade);
            Assert.Equal(entidade.Id, livroDto.Id);
            Assert.Equal(entidade.Titulo, livroDto.Titulo);
            Assert.Equal(entidade.Autor, livroDto.Autor);
            Assert.Equal(entidade.Categoria, livroDto.Categoria);
            Assert.Equal(entidade.Emissora, livroDto.Emissora);
            Assert.Equal(entidade.Quantidade, livroDto.Quantidade);
            Assert.Equal(entidade.Valor, livroDto.Valor);
            Assert.Equal(entidade.Ativo, livroDto.Ativo);
            Assert.Equal(entidade.DataLancamento, livroDto.DataLancamento);
        }
        public Livro NovoLivro()
        {
            return new Livro
            {
                Ativo = true,
                Autor = Faker.Name.FullName(),
                Categoria = Faker.Name.First(),
                DataAlteracao = null,
                DataCriacao = Faker.DateOfBirth.Next(),
                DataLancamento = Faker.DateOfBirth.Next(),
                Emissora = Faker.Company.Name(),
                Quantidade = Faker.RandomNumber.Next(),
                Titulo = Faker.Name.FullName(),
                Valor = Faker.RandomNumber.Next()
            };
        }
    }
}
