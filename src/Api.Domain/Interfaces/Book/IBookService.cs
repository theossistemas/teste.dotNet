using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dto;
using Api.Domain.Dto.Book;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Book
{
    public interface IBookService
    {
        Task<BookDto> Get(Guid id);
        Task<IEnumerable<BookDto>> GetAll();
        Task<BookDtoCreateResult> Post(BookDtoCreate book);
        Task<BookDtoUpdateResult> Put(BookDtoUpdate book);
        Task<bool> Delete(Guid id);
    }
}
