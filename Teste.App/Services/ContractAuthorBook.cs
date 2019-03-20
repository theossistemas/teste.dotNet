using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Teste.App.viewModel;
using Teste.Domain.Repositories;

namespace Teste.App.Services
{
    public class ContractAuthorBookApp
    {

        private readonly IAuthorBookRepository _rep;

        public ContractAuthorBookApp(IAuthorBookRepository rep)
        {
            _rep = rep;
        }


        public virtual AuthorBookViewModel GetById(int id)
        {
            try
            {
                var entity = _rep.Get(id);

                AuthorBookViewModel AuthorBook = new AuthorBookViewModel
                {
                    Id = entity.Id,
                    BookId = entity.BookId,
                    PersonId = entity.PersonId,
                    YearPublication = entity.YearPublication
                };
                return AuthorBook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual AuthorBook GetByName(int year)
        {
            return _rep.GetAll(null).Where(x => x.YearPublication == year).FirstOrDefault();
        }

        public virtual AuthorBook SaveAuthorBook(AuthorBookViewModel modelView)
        {
            try
            {
                AuthorBook entity = new AuthorBook
                {
                    BookId = modelView.BookId,
                    PersonId = modelView.PersonId,
                    YearPublication = modelView.YearPublication
                };
                _rep.Insert(entity);

                return entity;
            }
            catch
            {
                throw new ValidationException();
            }

        }

        public virtual AuthorBook EditAuthorBook(AuthorBookViewModel modelView)
        {
            try
            {
                AuthorBook AuthorBook = _rep.Get(modelView.Id.Value);

                if (AuthorBook != null)
                {
                    AuthorBook.BookId = modelView.BookId;
                    AuthorBook.PersonId = modelView.PersonId;
                    AuthorBook.YearPublication = modelView.YearPublication;
                    _rep.Update(AuthorBook);
                }

                return AuthorBook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAuthorBook(int id)
        {
            AuthorBook AuthorBook = _rep.Get(id);
            if (AuthorBook != null)
            {
                _rep.Delete(AuthorBook);
            }
        }

        public List<AuthorBook> GetAll()
        {
            //return _rep.GetAll(null).Where(x => (string.IsNullOrWhiteSpace(year) || x.YearPublication.ToUpper().Contains(nome.ToUpper()))).ToList();
            return _rep.GetAll(null).ToList();
        }


        public List<AuthorBook> GetAllAuthorBooks()
        {
            return _rep.getAllBook().ToList();
        }
    }
}
