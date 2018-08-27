using Livraria.Command.Notifications;
using Livraria.Domain.Entity;
using Livraria.Domain.Interface;
using Livraria.Domain.Interface.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Livraria.Command
{
    public class IncluirLivroHandler : LivroHandler, IRequestHandler<IncluirLivroCommand>
    {
        private readonly ILivroRepository _livroRepository;
        public IncluirLivroHandler(IUnitOfWork uow, INotificationHandler<Notification> notifications, IMediator mediator, ILivroRepository livroRepository, IAutorRepository autorRepository, IEditoraRepository editoraRepository) :
            base(uow, notifications, mediator, autorRepository, editoraRepository)
        {
            _livroRepository = livroRepository;
        }

        public Task Handle(IncluirLivroCommand request, CancellationToken cancellationToken)
        {
            if (!IsValidCommand(request))
                return Task.CompletedTask;

            VerificarAutor(request);
            VerificarEditora(request);

            var livro = new Livro(request.Nome, request.Descricao, request.Autor, request.Editora, request.Edicao);

            _livroRepository.Add(livro);

            Commit();

            return Task.CompletedTask;
        }
    }
}
