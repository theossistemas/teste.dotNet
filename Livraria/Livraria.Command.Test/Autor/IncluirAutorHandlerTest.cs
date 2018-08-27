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
    public class IncluirAutorHandlerTest
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IUnitOfWork _uow;
        private readonly INotificationHandler<Notification> _notifications;
        private readonly IMediator _mediator;

        public IncluirAutorHandlerTest()
        {
            _autorRepository = Substitute.For<IAutorRepository>();
            _uow = Substitute.For<IUnitOfWork>();
            _notifications = new NotificationHandler();
            _mediator = Substitute.For<IMediator>();
        }

        [TestMethod]
        public void NotificacaoNomeInconsistente()
        {
            //Arrange
            var handler = new IncluirAutorHandler(_uow, _notifications, _mediator, _autorRepository);
            var command = new IncluirAutorCommand(string.Empty);
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _mediator.ReceivedWithAnyArgs(2).Publish((INotification)null);
        }

        [TestMethod]
        public void NotificacaoNomeJaCadastrado()
        {
            //Arrange
            var nome = "Diego Matheus Porto";
            _autorRepository.IsNomeRegistered(nome).Returns(true);
            var handler = new IncluirAutorHandler(_uow, _notifications, _mediator, _autorRepository);
            var command = new IncluirAutorCommand(nome);
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _mediator.ReceivedWithAnyArgs(1).Publish((INotification)null);
        }

        [TestMethod]
        public void InclusaoRealizada()
        {
            //Arrange
            var nome = "Diego Matheus Porto";
            _autorRepository.IsNomeRegistered(nome).Returns(false);
            _uow.Commit().Returns(true);
            var handler = new IncluirAutorHandler(_uow, _notifications, _mediator, _autorRepository);
            var command = new IncluirAutorCommand(nome);
            //Act
            handler.Handle(command, new CancellationToken(false));
            //Assert
            _uow.Received().Commit();
        }
    }
}
