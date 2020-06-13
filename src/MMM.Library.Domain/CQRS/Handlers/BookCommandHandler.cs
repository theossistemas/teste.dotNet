using MediatR;
using MMM.Library.Domain.Core.Mediator;
using MMM.Library.Domain.Core.Models;
using MMM.Library.Domain.Core.Notifications;
using MMM.Library.Domain.CQRS.Commands;
using MMM.Library.Domain.CQRS.Events;
using MMM.Library.Domain.Interfaces;
using MMM.Library.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MMM.Library.Domain.CQRS.Handlers
{
    public class BookCommandHandler : CommandHandler,
        IRequestHandler<BookAddCommand, bool>,
        IRequestHandler<BookUpdateCommand, bool>,
        IRequestHandler<BookDeleteCommand, bool>

    {
        private readonly IBookRepository _bookRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public BookCommandHandler(IBookRepository bookRepository, IMediatorHandler mediatorHandler)
            : base(mediatorHandler)
        {
            _bookRepository = bookRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(BookAddCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            // Regras de Negócio
            var book = await _bookRepository.Find(book => book.Title.Contains(message.Title));

            // Verificar se livro existe
            if (book != null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Livro", "Livro Já Cadastrado!"));
                return false;
            }

            book = new Book(Guid.NewGuid(), message.CategoryId, message.Title, message.Year, message.Language, message.Location);

            _bookRepository.Add(book);

            if (await _bookRepository.UnitOfWork.Commit())
            {
                await _mediatorHandler.PublishEvent(new BookEventAdded(book.Id, book.CategoryId, book.Title, book.Year, book.Language, book.Location));
                return true;
            }

            await _mediatorHandler.PublishNotification(new DomainNotification("Erro", "Erro ao gravar dados!"));
            return false;
        }

        public async Task<bool> Handle(BookUpdateCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var book = await _bookRepository.GetById(message.Id);

            if (book == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Livro", "Livro Não Encontrado!"));
                return false;
            }

            book.UpdateBook(message.CategoryId, message.Title, message.Year, message.Language, message.Location);
            
            _bookRepository.Update(book);

            if (await _bookRepository.UnitOfWork.Commit())
            {
                await _mediatorHandler.PublishEvent(new BookEventUpdated(book.Id, book.CategoryId, book.Title, book.Year, book.Language, book.Location));
                return true;
            }

            await _mediatorHandler.PublishNotification(new DomainNotification("Erro", "Erro ao Atualizar dados!"));
            return false;
        }

        public async Task<bool> Handle(BookDeleteCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var book = await _bookRepository.GetById(message.Id);

            if (!await ObjectExists<Entity>(book)) return false;

            _bookRepository.Delete(book.Id);


            if (await _bookRepository.UnitOfWork.Commit())
            {
                await _mediatorHandler.PublishEvent(new BookEventDeleted(book.Id, book.CategoryId, book.Title, book.Year, book.Language, book.Location));
                return true;
            }
            
            await _mediatorHandler.PublishNotification(new DomainNotification("Erro", "Erro ao Remover dados!"));
            return false;   
        }

    }
}
