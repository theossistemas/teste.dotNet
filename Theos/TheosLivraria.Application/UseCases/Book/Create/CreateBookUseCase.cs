
using Microsoft.Extensions.Logging;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.CreateBook;
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.Create;
using TheosLivraria.Domain.Interfaces.Logs;
using TheosLivraria.Domain.Interfaces.Notification;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Interfaces.UnitOfWork;
using TheosLivraria.Domain.Models.Enum;

namespace TheosLivraria.Application.UseCases.Book.Create
{
    public class CreateBookUseCase(IBookRepository bookRepository, IUnitOfWork unitOfWork, IDomainNotification domainNotification, ILogger<CreateBookUseCase> log, IAuditService auditService) : ICreateBookUseCase
    {
        public async Task<ResponseCreateBookViewModel?> Execute(RequestCreateBookViewModel request)
        {
            log.LogInformation("Criando livro {Name}", request.Name);

            if (!Validate(request))
                return null;

            try
            {
                var newBook = request.ViewModelToModel(request);
                newBook.StatusId = (int)StatusEnum.Available;

                await bookRepository.AddAsync(newBook);
                await unitOfWork.CommitAsync();

                await auditService.LogInfoAsync($"Livro criado {newBook.Id}",nameof(CreateBookUseCase), Guid.NewGuid().ToString(), newBook);

                return new ResponseCreateBookViewModel { Id = newBook.Id };

            }
            catch (Exception ex)
            {
                log.LogError(ex, "Erro ao criar livro {Name}", request.Name);
                await auditService.LogErrorAsync(ex, "CreateBookUseCase", Guid.NewGuid().ToString(), request);
                return null;


            }
        }

        private bool Validate(RequestCreateBookViewModel request)
        {
            var validation = new CreateBookValidation();

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
    }
}
