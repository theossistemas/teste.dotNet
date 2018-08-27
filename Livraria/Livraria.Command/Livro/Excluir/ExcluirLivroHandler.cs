using Livraria.Command.Notifications;
using Livraria.Domain.Interface;
using Livraria.Domain.Interface.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Livraria.Command
{
    public class ExcluirLivroHandler : CommandHandler, IRequestHandler<ExcluirLivroCommand>
    {
        private readonly ILivroRepository _livroRepository;
        public ExcluirLivroHandler(IUnitOfWork uow, INotificationHandler<Notification> notifications, IMediator mediator, ILivroRepository livroRepository) : base(uow, notifications, mediator)
        {
            _livroRepository = livroRepository;
        }

        public Task Handle(ExcluirLivroCommand request, CancellationToken cancellationToken)
        {
            if (!IsValidCommand(request))
                return Task.CompletedTask;

            var id = new Guid(request.Id);

            var livro = _livroRepository.GetById(id);

            if (livro == null)
                return Notify(nameof(livro), "Livro não encontrado.");

            _livroRepository.Remove(id);

            Commit();

            return Task.CompletedTask;

        }
    }
}
