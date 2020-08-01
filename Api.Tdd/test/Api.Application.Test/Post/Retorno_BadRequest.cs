using Application.Controllers;
using Domain.Dtos;
using Domain.Interfaces.Services.CategoriaQuarto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.Post
{
    public class Retorno_BadRequest
    {
        private LivroController _controller;
        [Fact(DisplayName = "É possível Realizar o Created Bad Request.")]
        public async Task E_Possivel_Invocar_a_Controller_Create_BadRequest()
        {
            var serviceMock = new Mock<ILivroServices>();
            var serviceLogger = new Mock<ILogger<LivroController>>();
            serviceMock.Setup(m => m.Create(It.IsAny<LivroDto>())).ReturnsAsync(
               new LivroDto
               {
                   Ativo = true,
                   Autor = Faker.Name.FullName(),
                   Categoria = Faker.Name.First(),
                   DataLancamento = Faker.DateOfBirth.Next(),
                   Emissora = Faker.Company.Name(),
                   Quantidade = Faker.RandomNumber.Next(),
                   Titulo = Faker.Name.FullName(),
                   Valor = Faker.RandomNumber.Next()
               }
            );

            _controller = new LivroController(serviceMock.Object, serviceLogger.Object);
            _controller.ModelState.AddModelError("Titulo", "É um Campo Obrigatório");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var userDtoCreate = new LivroDto
            {
                Ativo = true,
                Autor = Faker.Name.FullName(),
                Categoria = Faker.Name.First(),
                DataLancamento = Faker.DateOfBirth.Next(),
                Emissora = Faker.Company.Name(),
                Quantidade = Faker.RandomNumber.Next(),
                Titulo = Faker.Name.FullName(),
                Valor = Faker.RandomNumber.Next()
            };

            var result = await _controller.Create(userDtoCreate);
            Assert.True(result is BadRequestObjectResult);
        }

        [Fact(DisplayName = "É possível Realizar o Created.")]
        public async Task E_Possivel_Invocar_a_Controller_Create()
        {
            var serviceMock = new Mock<ILivroServices>();
            var serviceLogger = new Mock<ILogger<LivroController>>();
            var livro = NovoLivro();
            serviceMock.Setup(m => m.Create(It.IsAny<LivroDto>())).ReturnsAsync(
               new LivroDto
               {
                   Ativo = livro.Ativo,
                   Autor = livro.Autor,
                   Categoria = livro.Categoria,
                   DataLancamento = livro.DataLancamento,
                   Emissora = livro.Emissora,
                   Quantidade = livro.Quantidade,
                   Titulo = livro.Titulo,
                   Valor = livro.Valor
               }
            );


            _controller = new LivroController(serviceMock.Object, serviceLogger.Object);            

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var livroDto = new LivroDto
            {
                Ativo = livro.Ativo,
                Autor = livro.Autor,
                Categoria = livro.Categoria,
                DataLancamento = livro.DataLancamento,
                Emissora = livro.Emissora,
                Quantidade = livro.Quantidade,
                Titulo = livro.Titulo,
                Valor = livro.Valor
            };

            var result = await _controller.Create(livroDto);
            Assert.True(result is CreatedResult);

            var resultValue = ((CreatedResult)result).Value as LivroDto;
            Assert.NotNull(resultValue);
            Assert.Equal(livroDto.Titulo, resultValue.Titulo);
            Assert.Equal(livroDto.Autor, resultValue.Autor);
        }
        public LivroDto NovoLivro()
        {
            return new LivroDto
            {
                Ativo = true,
                Autor = Faker.Name.FullName(),
                Categoria = Faker.Name.First(),
                DataLancamento = Faker.DateOfBirth.Next(),
                Emissora = Faker.Company.Name(),
                Quantidade = Faker.RandomNumber.Next(),
                Titulo = Faker.Name.FullName(),
                Valor = Faker.RandomNumber.Next()
            };
        }
    }
}
