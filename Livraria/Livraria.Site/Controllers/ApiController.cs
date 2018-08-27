using Livraria.Command.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Livraria.Site.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        private readonly NotificationHandler _notifications;

        protected ApiController(INotificationHandler<Notification> notifications)
        {
            _notifications = (NotificationHandler)notifications;
        }

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    data = result
                });
            }

            return BadRequest(new
            {
                errors = _notifications.GetNotifications().Select(n => new { n.Key, n.Value })
            });
        }
    }
}
