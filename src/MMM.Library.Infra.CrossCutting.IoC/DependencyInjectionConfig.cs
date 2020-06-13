using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MMM.Library.Application.Interfaces;
using MMM.Library.Application.Services;
using MMM.Library.Domain.Core.EvetSourcing;
using MMM.Library.Domain.Core.Interfaces;
using MMM.Library.Domain.Core.Mediator;
using MMM.Library.Domain.Core.Notifications;
using MMM.Library.Domain.CQRS.Commands;
using MMM.Library.Domain.CQRS.Events;
using MMM.Library.Domain.CQRS.Handlers;
using MMM.Library.Domain.CQRS.Queries;
using MMM.Library.Domain.Interfaces;
using MMM.Library.Infra.CrossCutting.Identity.Models;
using MMM.Library.Infra.CrossCutting.Logging.AspNetFilter;
using MMM.Library.Infra.CrossCutting.Logging.KissLogProvider;
using MMM.Library.Infra.Data.Context;
using MMM.Library.Infra.Data.EventSourcing;
using MMM.Library.Infra.Data.Repository;

namespace MMM.Library.Infra.CrossCutting.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Mediator         
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Application
            services.AddScoped<ICategoryAppService, CategoryAppService>();
            services.AddScoped<IBookAppService, BookAppService>();

            // Domain
            // Domain - Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<BookAddCommand, bool>, BookCommandHandler>();
            services.AddScoped<IRequestHandler<BookDeleteCommand, bool>, BookCommandHandler>();
            services.AddScoped<IRequestHandler<BookUpdateCommand, bool>, BookCommandHandler>();

            // Domain - Events
            services.AddScoped<INotificationHandler<BookEventAdded>, BookEventHandler>();
            services.AddScoped<INotificationHandler<BookEventUpdated>, BookEventHandler>();
            services.AddScoped<INotificationHandler<BookEventDeleted>, BookEventHandler>();

            // Domain - Queries
            services.AddScoped<IBookQueries, BookQueries>();

            // Infra - Data  
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();
            services.AddScoped<EventSourcingDbContext>();

            // Infra - CrossCutting - Identity  
            services.AddScoped<IUser, AspNetUser>();

            // EF Context ---
            services.AddScoped<LibraryDbContext>();

            // Infra - Filters
            services.AddScoped<AuditFilter>();
            services.AddScoped<GlobalActionLogger>();

            return services;
        }
    }
}
