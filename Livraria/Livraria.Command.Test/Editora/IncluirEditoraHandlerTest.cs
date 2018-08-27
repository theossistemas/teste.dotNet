using Livraria.Command.Notifications;
using Livraria.Domain.Interface;
using Livraria.Domain.Interface.Repositories;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Threading;

namespace Livraria.Command.Test
{
    [TestClass]
    public class IncluirEditoraHandlerTest
    {
        private readonly IEditoraRepository _editoraRepository;
        private readonly IUnitOfWork _uow;
        private readonly INotificationHandler<Notification> _notifications;
        private readonly IMediator _mediator;

        public IncluirEditoraHandlerTest()
        {
            _editoraRepository = Substitute.For<IEditoraRepository>();
            _uow = Substitute.For<IUnitOfWork>();
            _notifications = new NotificationHandler();
            _mediator = Substitute.For<IMediator>();
        }

        [TestMethod]
        public void NotificacaoNomeInconsistente()
        {
            //Arrange
            var handler = new IncluirEditoraHandler(_uow, _notifications, _mediator, _editoraRepository);
            var command = new IncluirEditoraCommand(string.Empty);
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _mediator.ReceivedWithAnyArgs(2).Publish((INotification)null);
        }

        [TestMethod]
        public void NotificacaoNomeJaCadastrado()
        {
            //Arrange
            var nome = "Editora Abril";
            _editoraRepository.IsNomeRegistered(nome).Returns(true);
            var handler = new IncluirEditoraHandler(_uow, _notifications, _mediator, _editoraRepository);
            var command = new IncluirEditoraCommand(nome);
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _mediator.ReceivedWithAnyArgs(1).Publish((INotification)null);
        }

        [TestMethod]
        public void InclusaoRealizada()
        {
            //Arrange
            var nome = "Editora Abril";
            _editoraRepository.IsNomeRegistered(nome).Returns(false);
            _uow.Commit().Returns(true);
            var handler = new IncluirEditoraHandler(_uow, _notifications, _mediator, _editoraRepository);
            var command = new IncluirEditoraCommand(nome);
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _uow.Received().Commit();
        }
    }
}
