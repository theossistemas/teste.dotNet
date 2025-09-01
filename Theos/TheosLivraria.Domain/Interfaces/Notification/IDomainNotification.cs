
using System.ComponentModel.DataAnnotations;
using TheosLivraria.Domain.Models.Notification;

namespace TheosLivraria.Domain.Interfaces.Notification
{
    public interface IDomainNotification
    {
        IReadOnlyCollection<NotificationMessage> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(string key, string message);
        void AddNotifications(IEnumerable<NotificationMessage> notifications);
    }
}
