using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theos.Challenge.Library.SharedKernel.DTO;
using Theos.Challenge.Library.SharedKernel.ValueObjects;

namespace Theos.Challenge.Library.Application.Contracts.Services
{    
    public interface IBookAppService
    {
        Task<int> Create(BookDTO book);
        Task<BookDTO> GetBookByIdentifier(Identifier identifier);
        Task<BookDTO> GetBookBy(Func<BookDTO,bool> condition);
        Task<IList<BookDTO>> GetAllBooksAsc();
        Task<bool> Update(BookDTO book);
        Task<bool> Delete(BookDTO book);
    }
}