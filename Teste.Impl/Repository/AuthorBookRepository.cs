using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teste.Domain.Repositories;
using Teste.Impl.Context;

namespace Teste.Impl.Repository
{
    public class AuthorBookRepository : Repository<AuthorBook>, IAuthorBookRepository
    {
        private readonly DataContext _dbContext;
        public AuthorBookRepository(DataContext dbContext)
       : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<AuthorBook> getAllBook()
        {
            IQueryable<AuthorBook> authorBook = _dbContext.Set<AuthorBook>().AsQueryable();

            IQueryable<Book> entityBook = _dbContext.Set<Book>().AsQueryable();

            IQueryable<Person> entityPerson = _dbContext.Set<Person>().AsQueryable();


            var query =
                from x in entityBook
                join ab in authorBook on x.Id equals ab.BookId
                from p in entityPerson.Where(p => p.Id == ab.PersonId).DefaultIfEmpty()
                //from e in entityBook.Where(b => b.Id == x.Id).DefaultIfEmpty()
                select new AuthorBook
                {
                    Id = ab.Id,
                    Book = ab == null ? null : new Book
                    {
                        Id = x.Id,
                        Edition = x.Edition,
                        Pages = x.Pages,
                        PublishingCompany = x.PublishingCompany,
                        Title = x.Title,
                        Url = x.Url
                    },
                    Person = p == null ? null : new Person
                    {
                        Id = p.Id,
                        BirthDate = p.BirthDate,
                        Cpf = p.Cpf,
                        Email = p.Email,
                        GenderId = p.GenderId,
                        Name = p.Name,
                    },
                  PersonId = p.Id,
                  BookId = ab.BookId,
                  YearPublication = ab.YearPublication
                };

            return query.ToList();
        }

    }
}
