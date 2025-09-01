
using Microsoft.Extensions.Logging;
using TheosLivraria.Application.UseCases.Book.Update;
using TheosLivraria.Domain.Interfaces.Logs;
using TheosLivraria.Domain.Interfaces.Notification;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Interfaces.UnitOfWork;

namespace TheosLivraria.Application.UseCases.Book.Delete
{
    public class DeleteBookUseCase(IBookRepository bookRepository, IUnitOfWork unitOfWork, IDomainNotification domainNotification, ILogger<UpdateBookUseCase> log, IAuditService auditService) : IDeleteBookUseCase
    {
        public async Task Execute(int Id)
        {
            log.LogInformation("Deletando livro {Id}", Id);

            if (!await BookExist(Id))
                return;

            var bookToDelete = await bookRepository.GetByIdAsync(Id);
            bookToDelete!.Active = false;

            try
            {
                bookRepository.Update(bookToDelete);
                await unitOfWork.CommitAsync();
                await auditService.LogInfoAsync($"Livro deletado {bookToDelete.Id}", "DeleteBookUseCase", Guid.NewGuid().ToString(), bookToDelete);
            
            }catch(Exception ex)
            {
                log.LogError(ex, "Erro ao deletar livro {Id}", Id);
                await auditService.LogErrorAsync(ex, "DeleteBookUseCase", Guid.NewGuid().ToString(), Id);
            }


        }

        private async Task<bool> BookExist(int id)
        {
            var book = await bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                domainNotification.AddNotification("Id", "Book not found.");
                return false;
            }
            return true;
        }
    }
}
