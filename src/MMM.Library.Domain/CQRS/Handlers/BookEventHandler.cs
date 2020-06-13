using MediatR;
using MMM.Library.Domain.CQRS.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MMM.Library.Domain.CQRS.Handlers
{
    public class BookEventHandler :
        INotificationHandler<BookEventAdded>,
        INotificationHandler<BookEventUpdated>,
        INotificationHandler<BookEventDeleted>
    {

        // Eventos - enviar notificações
        public Task Handle(BookEventAdded notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;    
        }

        public Task Handle(BookEventUpdated notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(BookEventDeleted notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
