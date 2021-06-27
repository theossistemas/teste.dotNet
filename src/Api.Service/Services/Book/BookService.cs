using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dto;
using Api.Domain.Dto.Book;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Book;
using AutoMapper;

namespace Api.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<BookEntity> _repository;

        private readonly IMapper _mapper;

        public BookService(IRepository<BookEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<BookDto> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id);
            return _mapper.Map<BookDto>(entity);
        }
        public async Task<IEnumerable<BookDto>> GetAll()
        {
            var listEntity = await _repository.SelectAsync();
            var dto = _mapper.Map<IEnumerable<BookDto>>(listEntity);
            return dto;
        }
        public async Task<BookDtoCreateResult> Post(BookDtoCreate book)
        {
            var entity = _mapper.Map<BookEntity>(book);
            var exist = await ExistByName(book.Title);
            if (exist)
            {
                throw new ArgumentException("Book already registered");
            }
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<BookDtoCreateResult>(result);
        }
        public async Task<BookDtoUpdateResult> Put(BookDtoUpdate book)
        {
            var entity = _mapper.Map<BookEntity>(book);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<BookDtoUpdateResult>(result);
        }

        public async Task<bool> ExistByName(string nameBook)
        {
            var listBooks = await _repository.SelectAsync();
            foreach (var item in listBooks)
            {
                if (item.Title.Equals(nameBook))
                {
                    return true;
                }
            }
            return false;

        }
    }
}

