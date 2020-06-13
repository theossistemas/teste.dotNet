using MMM.IStore.Core.Messages;
using MMM.Library.Domain.Core.Messages;
using MMM.Library.Domain.Core.Notifications;
using System.Threading.Tasks;

namespace MMM.Library.Domain.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T theEvent) where T : Event;
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
        Task PublishDomainEvent<T>(T notification) where T : DomainEvent;
    }
}
