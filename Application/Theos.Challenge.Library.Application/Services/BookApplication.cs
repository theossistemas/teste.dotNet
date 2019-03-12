using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theos.Challenge.Library.Model.Contracts.Services;
using Theos.Challenge.Library.SharedKernel.Entities;
using Theos.Challenge.Library.SharedKernel.ValueObjects;

namespace Theos.Challenge.Library.Application.Services
{
    public class BookApplicationService
    {
        private readonly IBookDomainService _domainService;

        public BookApplicationService(IBookDomainService domainService)
        {
            _domainService = domainService;
        }

        public Task<int> Create(Book book){
            //Validations Here
            return _domainService.Create(book);
        }

        public Task<bool> Delete(Book book)
        {
            //Validation Here
            return _domainService.Delete(book);
        }

        public Task<IList<Book>> GetAllBooksAsc()
        {
            //Validation Here
            return _domainService.GetAllBooksAsc();
        }

        public Task<Book> GetBookBy(Func<Book, bool> condition)
        {
            //Validation Here
            return _domainService.GetBookBy(condition);
        }

        public Task<Book> GetBookByIdentifier(Identifier identifier)
        {
            //Validation Here
            return _domainService.GetBookByIdentifier(identifier);
        }

        public Task<bool> Update(Book book)
        {
            return _domainService.Update(book);
        }
    }
}