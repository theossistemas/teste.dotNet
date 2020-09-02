using Livraria.Common.Handler;
using Livraria.Common.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace Livraria.API.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly NotifiyHandler _messageHandler;

        protected BaseController(INotificationHandler<Notifications> notification)
        {
            _messageHandler = (NotifiyHandler)notification;
        }

        protected bool HasNotifications()
        {
            return _messageHandler.HasNotifications();
        }

        protected bool OperacaoValida() => !_messageHandler.HasNotifications();

        protected BadRequestObjectResult BadRequestResponse()
        {
            return BadRequest(_messageHandler.GetNotifications().Select(n => n));
        }
    }
}
