using Architecture;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : BaseControllerCrud<Book, BookDTO, BookDTO, BookDTO>
    {
        public BookController(IServiceBase<Book> service, IMapper mapper, ILogger<BaseController<Book, BookDTO>> logger) : base(service, mapper, logger)
        {
        }
    }
}
