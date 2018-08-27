using Livraria.Command.Notifications;
using Livraria.Domain.Entity;
using Livraria.Domain.Interface;
using Livraria.Domain.Interface.Repositories;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Threading;

namespace Livraria.Command.Test
{
    [TestClass]
    public class ExcluirLivroHandlerTest
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IUnitOfWork _uow;
        private readonly INotificationHandler<Notification> _notifications;
        private readonly IMediator _mediator;
        private readonly Livro _livro = new Livro(
            "The Hobbit",
            "The classic bestseller behind this year's biggest movie, this definitive paperback edition features nine illustrations and two maps drawn by J.R.R. Tolkien, and a preface by Christopher Tolkien. Bilbo Baggins is a hobbit who enjoys a comfortable, unambitious life, rarely travelling further than the pantry of his hobbit-hole in Bag End. But his contentment is disturbed when the wizard, Gandalf, and a company of thirteen dwarves arrive on his doorstep one day to whisk him away on an unexpected journey 'there and back again'. They have a plot to raid the treasure hoard of Smaug the Magnificent, a large and very dangerous dragon! The prelude to The Lord of the Rings, The Hobbit has sold many millions of copies since its publication in 1937, establishing itself as one of the most beloved and influential books of the twentieth century.",
            new Autor("J. R. R. Tolkien"),
            new Editora("Harpercollins Uk"),
            1
            );

        public ExcluirLivroHandlerTest()
        {
            _livroRepository = Substitute.For<ILivroRepository>();
            _uow = Substitute.For<IUnitOfWork>();
            _notifications = new NotificationHandler();
            _mediator = Substitute.For<IMediator>();
        }

        [TestMethod]
        public void NotificacaoIdInconsistente()
        {
            //Arrange
            var handler = new ExcluirLivroHandler(_uow, _notifications, _mediator, _livroRepository);
            var command = new ExcluirLivroCommand(string.Empty);
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _mediator.ReceivedWithAnyArgs(2).Publish((INotification)null);
        }

        [TestMethod]
        public void NotificacaoLivroNaoEncontrado()
        {
            //Arrange
            _livroRepository.GetById(_livro.Id).ReturnsForAnyArgs((Livro)null);
            var handler = new ExcluirLivroHandler(_uow, _notifications, _mediator, _livroRepository);
            var command = new ExcluirLivroCommand(_livro.Id.ToString());
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _mediator.ReceivedWithAnyArgs(1).Publish((INotification)null);
        }

        [TestMethod]
        public void ExclusaoRealizada()
        {
            //Arrange
            _livroRepository.GetById(_livro.Id).ReturnsForAnyArgs(_livro);
            _uow.Commit().Returns(true);
            var handler = new ExcluirLivroHandler(_uow, _notifications, _mediator, _livroRepository);
            var command = new ExcluirLivroCommand(_livro.Id.ToString());
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _uow.Received().Commit();
        }
    }
}
