using Livraria.Command.Notifications;
using Livraria.Domain.Interface;
using Livraria.Domain.Interface.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Livraria.Command
{
    public class AlterarLivroHandler : LivroHandler, IRequestHandler<AlterarLivroCommand>
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IAutorRepository _autorRepository;
        private readonly IEditoraRepository _editoraRepository;

        public AlterarLivroHandler(IUnitOfWork uow, INotificationHandler<Notification> notifications, IMediator mediator, ILivroRepository livroRepository, IAutorRepository autorRepository, IEditoraRepository editoraRepository) :
            base(uow, notifications, mediator, autorRepository, editoraRepository)
        {
            _livroRepository = livroRepository;
            _autorRepository = autorRepository;
            _editoraRepository = editoraRepository;
        }

        public Task Handle(AlterarLivroCommand request, CancellationToken cancellationToken)
        {
            if (!IsValidCommand(request))
                return Task.CompletedTask;

            var livro = _livroRepository.GetById(new Guid(request.Id));

            if (livro == null)
                return Notify(nameof(livro), "Livro não encontrado.");

            VerificarAutor(request);
            VerificarEditora(request);

            livro.Nome = request.Nome;
            livro.Descricao = request.Descricao;
            livro.Autor = request.Autor;
            livro.Editora = request.Editora;
            livro.Edicao = request.Edicao;

            _livroRepository.Update(livro);

            Commit();

            return Task.CompletedTask;
        }
    }
}
