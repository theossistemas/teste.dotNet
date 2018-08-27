using Livraria.Command.Notifications;
using Livraria.Domain.Entity;
using Livraria.Domain.Interface;
using Livraria.Domain.Interface.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Livraria.Command
{
    public class IncluirAutorHandler : CommandHandler, IRequestHandler<IncluirAutorCommand>
    {
        private readonly IAutorRepository _autorRepository;

        public IncluirAutorHandler(IUnitOfWork uow, INotificationHandler<Notification> notifications, IMediator mediator, IAutorRepository autorRepository) : base(uow, notifications, mediator)
        {
            _autorRepository = autorRepository;
        }

        public Task Handle(IncluirAutorCommand request, CancellationToken cancellationToken)
        {
            if (!IsValidCommand(request))
                return Task.CompletedTask;

            if (_autorRepository.IsNomeRegistered(request.Nome))
                return Notify(nameof(request.Nome), "Nome já cadastrado.");

            var autor = new Autor(request.Nome);

            _autorRepository.Add(autor);

            Commit();

            return Task.CompletedTask;
        }
    }
}
