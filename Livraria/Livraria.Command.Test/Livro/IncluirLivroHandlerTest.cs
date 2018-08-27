using Livraria.Command.Notifications;
using Livraria.Domain.Entity;
using Livraria.Domain.Interface;
using Livraria.Domain.Interface.Repositories;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Threading;

namespace Livraria.Command.Test
{
    [TestClass]
    public class IncluirLivroHandlerTest
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IAutorRepository _autorRepository;
        private readonly IEditoraRepository _editoraRepository;
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

        public IncluirLivroHandlerTest()
        {
            _livroRepository = Substitute.For<ILivroRepository>();
            _autorRepository = Substitute.For<IAutorRepository>();
            _editoraRepository = Substitute.For<IEditoraRepository>();
            _uow = Substitute.For<IUnitOfWork>();
            _notifications = new NotificationHandler();
            _mediator = Substitute.For<IMediator>();
        }

        [TestMethod]
        public void NotificacaoNomeInconsistente()
        {
            //Arrange
            var handler = new IncluirLivroHandler(_uow, _notifications, _mediator, _livroRepository, _autorRepository, _editoraRepository);
            var command = new IncluirLivroCommand(string.Empty, _livro.Descricao, _livro.Autor, _livro.Editora, _livro.Edicao);
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _mediator.ReceivedWithAnyArgs(2).Publish((INotification)null);
        }

        [TestMethod]
        public void NotificacaoDescricaoInconsistente()
        {
            //Arrange
            var handler = new IncluirLivroHandler(_uow, _notifications, _mediator, _livroRepository, _autorRepository, _editoraRepository);
            var command = new IncluirLivroCommand(_livro.Nome, string.Empty, _livro.Autor, _livro.Editora, _livro.Edicao);
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _mediator.ReceivedWithAnyArgs(1).Publish((INotification)null);
        }

        [TestMethod]
        public void NotificacaoAutorInconsistente()
        {
            //Arrange
            var handler = new IncluirLivroHandler(_uow, _notifications, _mediator, _livroRepository, _autorRepository, _editoraRepository);
            var command = new IncluirLivroCommand(_livro.Nome, _livro.Descricao, null, _livro.Editora, _livro.Edicao);
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _mediator.ReceivedWithAnyArgs(1).Publish((INotification)null);
        }

        [TestMethod]
        public void NotificacaoEditoraInconsistente()
        {
            //Arrange
            var handler = new IncluirLivroHandler(_uow, _notifications, _mediator, _livroRepository, _autorRepository, _editoraRepository);
            var command = new IncluirLivroCommand(_livro.Nome, _livro.Descricao, _livro.Autor, null, _livro.Edicao);
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _mediator.ReceivedWithAnyArgs(1).Publish((INotification)null);
        }

        [TestMethod]
        public void NotificacaoEdicaoInconsistente()
        {
            //Arrange
            var handler = new IncluirLivroHandler(_uow, _notifications, _mediator, _livroRepository, _autorRepository, _editoraRepository);
            var command = new IncluirLivroCommand(_livro.Nome, _livro.Descricao, _livro.Autor, _livro.Editora, 0);
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _mediator.ReceivedWithAnyArgs(1).Publish((INotification)null);
        }

        [TestMethod]
        public void InclusaoRealizada()
        {
            //Arrange
            _uow.Commit().Returns(true);
            var handler = new IncluirLivroHandler(_uow, _notifications, _mediator, _livroRepository, _autorRepository, _editoraRepository);
            var command = new IncluirLivroCommand(_livro.Nome, _livro.Descricao, _livro.Autor, _livro.Editora, _livro.Edicao);
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _uow.Received().Commit();
        }
    }
}
