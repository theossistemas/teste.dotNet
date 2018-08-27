using Livraria.Command.Notifications;
using Livraria.Domain.Entity;
using Livraria.Domain.Interface;
using Livraria.Domain.Interface.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Livraria.Command
{
    public class IncluirEditoraHandler : CommandHandler, IRequestHandler<IncluirEditoraCommand>
    {
        private readonly IEditoraRepository _editoraRepository;
        public IncluirEditoraHandler(IUnitOfWork uow, INotificationHandler<Notification> notifications, IMediator mediator, IEditoraRepository editoraRepository) : base(uow, notifications, mediator)
        {
            _editoraRepository = editoraRepository;
        }

        public Task Handle(IncluirEditoraCommand request, CancellationToken cancellationToken)
        {
            if (!IsValidCommand(request))
                return Task.CompletedTask;

            if (_editoraRepository.IsNomeRegistered(request.Nome))
                return Notify(nameof(request.Nome), "Nome já cadastrado.");

            var editora = new Editora(request.Nome);

            _editoraRepository.Add(editora);

            Commit();

            return Task.CompletedTask;
        }
    }
}
