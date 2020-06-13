using MMM.IStore.Core.Messages;
using MMM.Library.Domain.Core.Mediator;
using MMM.Library.Domain.Core.Models;
using MMM.Library.Domain.Core.Notifications;
using System;
using System.Threading.Tasks;

namespace MMM.Library.Domain.CQRS.Handlers
{
    public abstract class CommandHandler
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CommandHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected async Task<bool> ObjectExists<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (entity != null) return true;

            await _mediatorHandler.PublishNotification(new DomainNotification(entity.GetType().Name,
               $"{entity.GetType().Name} não Encontrado!"));

            return true;
        }

        protected bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
