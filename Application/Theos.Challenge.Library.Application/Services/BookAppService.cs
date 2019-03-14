using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Theos.Challenge.Library.Application.Contracts.Services;
using Theos.Challenge.Library.Model.Contracts.Services;
using Theos.Challenge.Library.SharedKernel.DTO;
using Theos.Challenge.Library.SharedKernel.Entities;
using Theos.Challenge.Library.SharedKernel.ValueObjects;

namespace Theos.Challenge.Library.Application.Services
{
    public class BookAppService: IBookAppService
    {
        private readonly IBookDomainService _domainService;
        private readonly IMapper _mapper;

        public BookAppService(IBookDomainService domainService, IMapper mapper)
        {
            _domainService = domainService;
            _mapper = mapper;
        }        

        public async Task<int> Create(BookDTO book)
        {               
            return await _domainService.Create(_mapper.Map<Book>(book));
        }

        public async Task<int> Delete(BookDTO book)
        {            
            return await _domainService.Delete(_mapper.Map<Book>(book));
        }

        public Task<IList<BookDTO>> GetAllBooksAsc()
        {
            throw new NotImplementedException();
        }

        public Task<BookDTO> GetBookBy(Func<BookDTO, bool> condition)
        {
            throw new NotImplementedException();
        }

        public Task<BookDTO> GetBookByIdentifier(Identifier identifier)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(BookDTO book)
        {
            return _domainService.Update(_mapper.Map<Book>(book));
        }
    }
}