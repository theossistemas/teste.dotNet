using Livraria.Command.Notifications;
using Livraria.Domain.Entity;
using Livraria.Domain.Interface;
using Livraria.Domain.Interface.Repositories;
using MediatR;

namespace Livraria.Command
{
    public abstract class LivroHandler : CommandHandler
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IEditoraRepository _editoraRepository;

        public LivroHandler(IUnitOfWork uow, INotificationHandler<Notification> notifications, IMediator mediator, IAutorRepository autorRepository, IEditoraRepository editoraRepository) : base(uow, notifications, mediator)
        {
            _autorRepository = autorRepository;
            _editoraRepository = editoraRepository;
        }

        protected void VerificarEditora(LivroCommand livro)
        {
            if (livro.Editora.Id != null && _editoraRepository.HasId(livro.Editora.Id))
                livro.Editora = _editoraRepository.GetById(livro.Editora.Id);
            else if (!_editoraRepository.IsNomeRegistered(livro.Editora.Nome))
                Mediator.Send(new IncluirEditoraCommand(livro.Editora.Nome)).Wait();

            livro.Editora = _editoraRepository.BuscarPorNome(livro.Editora.Nome);
        }

        protected void VerificarAutor(LivroCommand livro)
        {
            if (livro.Autor.Id != null && _autorRepository.HasId(livro.Autor.Id))
                livro.Autor = _autorRepository.GetById(livro.Autor.Id);
            else if (!_autorRepository.IsNomeRegistered(livro.Autor.Nome))
                Mediator.Send(new IncluirAutorCommand(livro.Autor.Nome)).Wait();

            livro.Autor = _autorRepository.BuscarPorNome(livro.Autor.Nome);
        }
    }
}
