using Architecture;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BookService : ServiceBase<Book>, IBookService
    {
        public BookService(IRepositoryBase<Book> repository) : base(repository)
        {
        }
    }
}
