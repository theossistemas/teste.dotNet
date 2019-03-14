using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theos.Challenge.Library.Model.Contracts.Infra;
using Theos.Challenge.Library.Model.Contracts.Services;
using Theos.Challenge.Library.SharedKernel.Entities;
using Theos.Challenge.Library.SharedKernel.ValueObjects;

namespace Theos.Challenge.Library.Model.Services
{
    public class BookDomainService: IBookDomainService
    {
        private readonly IBookRepository _repository;

        public BookDomainService(IBookRepository repository)
        {
            _repository = repository;
        }

        public Task<int> Create(Book book){
            //Validations Here
            return _repository.Create(book);
        }

        public Task<int> Delete(Book book)
        {
            //Validation Here
            return _repository.Delete(book);
        }

        public Task<IList<Book>> GetAllBooksAsc()
        {
            //Validation Here
            return _repository.GetAllBooks();
        }

        public Task<Book> GetBookBy(Func<Book, bool> condition)
        {
            //Validation Here
            return _repository.GetBookBy(condition);
        }

        public Task<Book> GetBookByIdentifier(Identifier identifier)
        {
            //Validation Here
            return _repository.GetBookByIdentifier(identifier);
        }

        public Task<int> Update(Book book)
        {
            return _repository.Update(book);
        }
    }
}