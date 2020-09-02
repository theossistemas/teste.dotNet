using FluentValidation.Results;
using Livraria.Common.Handler;
using Livraria.Common.Interface;
using Livraria.Common.Model;
using Livraria.Common.Utils;
using MediatR;
using System.Threading;

namespace Livraria.Common.Implementation
{
    public class Notify : INotify
    {
        private readonly NotifiyHandler _messageHandler;

        public Notify(INotificationHandler<Notifications> notification)
        {
            _messageHandler = (NotifiyHandler)notification;
        }

        public Notify Invoke()
        {
            return this;
        }

        public bool IsValid()
        {
            return !_messageHandler.HasNotifications();
        }

        public void NewNotification(string key, string message)
        {
            _messageHandler.Handle(new Notifications(key, message), default(CancellationToken));
        }

        public void NewNotification(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
                _messageHandler.Handle(new Notifications(Resources.ErroDeDominio, erro.ErrorMessage), default(CancellationToken));
        }
    }
}
