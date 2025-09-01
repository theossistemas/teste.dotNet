
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheosLivraria.Application.UseCases.Book.Create;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.Update;
using TheosLivraria.Domain.Interfaces.Logs;
using TheosLivraria.Domain.Interfaces.Notification;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Interfaces.UnitOfWork;

namespace TheosLivraria.Application.UseCases.Book.Update
{
    public class UpdateBookUseCase(IBookRepository bookRepository, IUnitOfWork unitOfWork ,IDomainNotification domainNotification, ILogger<UpdateBookUseCase> log, IAuditService auditService) : IUpdateBookUseCase
    {
        public async Task Execute(int Id, RequestUpdateBookViewModel request)
        {
            log.LogInformation("Atualizando livro {Id}", Id);

            if (!await Validate(Id, request))
                return;

            var bookToUpdate = await bookRepository.GetByIdAsync(Id);
            bookToUpdate!.Name = request.Name;
            bookToUpdate.Author = request.Author;
            bookToUpdate.StatusId = request.StatusId;

            try
            {
                bookRepository.Update(bookToUpdate);
                await unitOfWork.CommitAsync();
                await auditService.LogInfoAsync($"Livro atualizado {bookToUpdate.Id}", "UpdateBookUseCase", Guid.NewGuid().ToString(), bookToUpdate);
            
            }catch(Exception ex)
            {
                log.LogError(ex, "Erro ao atualizar livro {Id}", Id);
                await auditService.LogErrorAsync(ex, "UpdateBookUseCase", Guid.NewGuid().ToString(), request);
            }

        }

        private async Task<bool> Validate(int id, RequestUpdateBookViewModel request)
        {
            if(! await BookExists(id))
                return false;


            var validation = new UpdateBookValidation();
            if(!validation.Validate(request).IsValid)
            {
                foreach (var error in validation.Validate(request).Errors)
                {
                    domainNotification.AddNotification(error.PropertyName, error.ErrorMessage);
                }
                return false;
            }
                
            return true;
        }

        private async Task<bool> BookExists(int Id)
        {
            var book = await bookRepository.GetByIdAsync(Id);
            if (book == null)
            {
                domainNotification.AddNotification("Id", "Book not found.");
                return false;
            }
            return true;
        }
    }
}
