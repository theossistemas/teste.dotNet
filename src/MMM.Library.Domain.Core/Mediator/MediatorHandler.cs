using MediatR;
using MMM.IStore.Core.Messages;
using MMM.Library.Domain.Core.EvetSourcing;
using MMM.Library.Domain.Core.Messages;
using MMM.Library.Domain.Core.Notifications;
using System.Threading.Tasks;

namespace MMM.Library.Domain.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public MediatorHandler(IMediator mediator,
                IEventSourcingRepository eventSourcingRepository)
        {
            _mediator = mediator;
            _eventSourcingRepository = eventSourcingRepository;
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }

        public async Task PublishEvent<T>(T theEvent) where T : Event
        {
            await _mediator.Publish(theEvent);
            await _eventSourcingRepository.StoreEvent(theEvent);
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }

        public async Task PublishDomainEvent<T>(T notification) where T : DomainEvent
        {
            await _mediator.Publish(notification);
        }
    }
}
