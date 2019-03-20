using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Teste.App.viewModel;

namespace Teste.App.Services
{
    public class ContractBookApp
    {
        private IRepository<Book> _rep;
        private IRepository<AuthorBook> _repAuthorBook;

        public ContractBookApp(IRepository<Book> rep, IRepository<AuthorBook> repAuthorBook)
        {
            _rep = rep;
            _repAuthorBook = repAuthorBook;
        }

        public virtual BookViewModel GetById(int id)
        {
            try
            {
                var entity = _rep.Get(id);

                BookViewModel book = new BookViewModel
                {
                    Title = entity.Title,
                    Edition = entity.Edition,
                    Pages = entity.Pages,
                    PublishingCompany = entity.PublishingCompany,
                };
                return book;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual Book GetByName(string name)
        {
            return _rep.GetAll(null).Where(x => x.Title == name).FirstOrDefault();
        }

        public virtual Book SaveBook(BookViewModel modelView)
        {
            try
            {
               
                Book entity = new Book
                {
                    Title = modelView.Title,
                    Edition = modelView.Edition,
                    Pages = modelView.Pages,
                    PublishingCompany = modelView.PublishingCompany,
                    Url = modelView.Url
                };
                entity = _rep.Insert(entity);

                AuthorBook authorBook = new AuthorBook
                {
                    BookId = entity.Id,
                    PersonId = modelView.PersonId,
                    YearPublication = modelView.YearPublication
                };

                _repAuthorBook.Insert(authorBook);
                return entity;
            }
            catch (Exception ex)
            {
                throw new ValidationException();
            }

        }

        public virtual Book EditBook(BookViewModel modelView)
        {
            try
            {
                Book book = _rep.Get(modelView.Id.Value);
                AuthorBook authorBook = _repAuthorBook.Get(modelView.AuthorBookId);

                authorBook.PersonId = modelView.PersonId;
                authorBook.YearPublication = modelView.YearPublication;

                _repAuthorBook.Update(authorBook);
                if (book != null)
                {
                    book.Title = modelView.Title;
                    book.Edition = modelView.Edition;
                    book.Pages = modelView.Pages;
                    book.PublishingCompany = modelView.PublishingCompany;
                    _rep.Update(book);
                }

                return book;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteBook(int id)
        {
            Book Person = _rep.Get(id);
            if (Person != null)
            {
                _rep.Delete(Person);
            }
        }

        public List<Book> GetAll(string nome)
        {
            return _rep.GetAll(nome).Where(x => (string.IsNullOrWhiteSpace(nome) || x.Title.ToUpper().Contains(nome.ToUpper()))).ToList();
        }
    }
}
