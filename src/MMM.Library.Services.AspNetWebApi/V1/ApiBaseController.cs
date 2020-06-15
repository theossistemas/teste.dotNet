using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MMM.Library.Domain.Core.Mediator;
using MMM.Library.Domain.Core.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMM.Library.Services.AspNetWebApi.V1
{
    public abstract class ApiBaseController : ControllerBase
    {
        protected readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediatorHandler;

        //public readonly IUser _AppUser;
        protected Guid UserId { get; set; }
        protected bool AuthenticatedUser { get; set; }

        protected ApiBaseController(INotificationHandler<DomainNotification> notifications,
                                    IMediatorHandler mediatorHandler) //,IUser appUser)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
            ////_AppUser = appUser;

            //if (appUser.IsAuthenticated())
            //{
            //    //UserId = appUser.GetUserId();
            //    AuthenticatedUser = true;
            //}
        }       

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected bool IsValidOperation()
        {
            return !_notifications.HasNotifications();
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifierInvalidModelErrors(modelState);

            return CustomResponse();
        }

        protected void NotifierInvalidModelErrors(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in erros)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError("error", errorMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(code, message));
        }

        protected IEnumerable<string> GetErrorMessages()
        {
            return _notifications.GetNotifications().Select(c => c.Value).ToList();
        }

           
    }
}
