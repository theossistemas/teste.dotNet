using AutoMapper;
using FluentAssertions;
using Livraria.Command;
using Livraria.Domain.Entity;
using Livraria.Domain.Interface.QueryRepositories;
using Livraria.Service.Classes;
using Livraria.Service.DTOs;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Livraria.Service.Test
{
    public class LivroServiceTest
    {
        private readonly IMapper _mapper;
        private readonly ILivroQueryRepository _livroQueryRepository;
        private readonly IMediator _mediator;
        private readonly Livro _livro = new Livro(
            "The Hobbit",
            "The classic bestseller behind this year's biggest movie, this definitive paperback edition features nine illustrations and two maps drawn by J.R.R. Tolkien, and a preface by Christopher Tolkien. Bilbo Baggins is a hobbit who enjoys a comfortable, unambitious life, rarely travelling further than the pantry of his hobbit-hole in Bag End. But his contentment is disturbed when the wizard, Gandalf, and a company of thirteen dwarves arrive on his doorstep one day to whisk him away on an unexpected journey 'there and back again'. They have a plot to raid the treasure hoard of Smaug the Magnificent, a large and very dangerous dragon! The prelude to The Lord of the Rings, The Hobbit has sold many millions of copies since its publication in 1937, establishing itself as one of the most beloved and influential books of the twentieth century.",
            new Autor("J. R. R. Tolkien"),
            new Editora("Harpercollins Uk"),
            1
            );

        public LivroServiceTest()
        {
            _mapper = Substitute.For<IMapper>();
            _livroQueryRepository = Substitute.For<ILivroQueryRepository>();
            _mediator = Substitute.For<IMediator>();
        }

        [TestMethod]
        public void Alterar()
        {
            //Arrange
            var service = new LivroService(_mapper, _livroQueryRepository, _mediator);
            //Act
            service.Alterar(new LivroDTO());
            //Assert
            _mediator.Received(1).Send(Arg.Any<AlterarLivroCommand>());
        }

        [TestMethod]
        public void Consultar()
        {
            //Arrange
            _livroQueryRepository.GetById(_livro.Id.ToString()).Returns(_livro);
            var service = new LivroService(_mapper, _livroQueryRepository, _mediator);
            //Act
            var livro = service.Consultar(_livro.Id.ToString());
            //Assert
            livro.Should().BeEquivalentTo(_livro);
        }

        public void Excluir(string id)
        {
            //Arrange
            var service = new LivroService(_mapper, _livroQueryRepository, _mediator);
            //Act
            service.Excluir(_livro.Id.ToString());
            //Assert
            _mediator.Received(1).Send(Arg.Any<ExcluirLivroCommand>());
        }

        public void Incluir(LivroDTO livroDTO)
        {
            //Arrange
            var service = new LivroService(_mapper, _livroQueryRepository, _mediator);
            //Act
            service.Incluir(new LivroDTO());
            //Assert
            _mediator.Received(1).Send(Arg.Any<IncluirLivroCommand>());
        }

        public void Listar()
        {
            //Arrange
            var service = new LivroService(_mapper, _livroQueryRepository, _mediator);
            //Act
            service.Listar();
        }

        public void ListarOrdenadoPorNome()
        {
            //Arrange
            var service = new LivroService(_mapper, _livroQueryRepository, _mediator);
            //Act
            service.ListarOrdenadoPorNome();
        }
    }
}
