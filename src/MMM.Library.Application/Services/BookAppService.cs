using AutoMapper;
using MediatR;
using MMM.Library.Application.Interfaces;
using MMM.Library.Application.ViewModels;
using MMM.Library.Domain.CQRS.Commands;
using MMM.Library.Domain.Interfaces;
using MMM.Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Application.Services
{
    public class BookAppService : IBookAppService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BookAppService(IBookRepository productRepository, IMapper mapper, IMediator mediator)
        {
            _bookRepository = productRepository;
            _mapper = mapper;
            _mediator = mediator;
        }


        // CQRS Commands
        public async Task AddBook(BookViewModel bookViewModel)
        {
            var command = _mapper.Map<BookAddCommand>(bookViewModel);
            await _mediator.Send(command);
        }

        public async Task UpdateBook(BookViewModel bookViewModel)
        {
            var command = _mapper.Map<BookUpdateCommand>(bookViewModel);
            await _mediator.Send(command);
        }

        public async Task DeleteBook(Guid id)
        {
            var command = new BookDeleteCommand(id);
            await _mediator.Send(command);
        }


        // Queries com AutoMapper
        public async Task<BookViewModel> GetById(Guid id)
        {
            return _mapper.Map<BookViewModel>(await _bookRepository.GetById(id));
        }

        public async Task<IEnumerable<BookViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<BookViewModel>>(await _bookRepository.GetAllBooksWithCategoryAndAuthor());
        }

        public async Task<IEnumerable<BookViewModel>> GetByCategory(int code)
        {
            return _mapper.Map<IEnumerable<BookViewModel>>(await _bookRepository.GetBookByCategory(code));
        }
    }
}
