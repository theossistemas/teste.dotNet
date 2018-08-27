using Livraria.Command.Notifications;
using Livraria.Domain.Interface;
using MediatR;
using System.Threading.Tasks;

namespace Livraria.Command
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly NotificationHandler _notifications;
        protected readonly IMediator Mediator;
        public CommandHandler(IUnitOfWork uow, INotificationHandler<Notification> notifications, IMediator mediator)
        {
            _uow = uow;
            _notifications = (NotificationHandler)notifications;
            Mediator = mediator;
        }

        protected bool IsValidCommand(Command message)
        {
            if (message.IsValid())
                return true;
            foreach (var error in message.ValidationResult.Errors)
            {
                Mediator.Publish(new Notification(error.PropertyName, error.ErrorMessage));
            }
            return false;
        }

        protected Task Notify(string key, string value)
        {
            Mediator.Publish(new Notification(key, value));
            return Task.CompletedTask;
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            if (_uow.Commit()) return true;

            Mediator.Publish(new Notification("Commit", "Houve alguma falha ao salvar as informações."));
            return false;
        }
    }
}
