using LibraryStore.Core.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryStore.Core.Business
{
    public interface IBusiness<TDto, TInputDto>
        where TDto : class, IDto
        where TInputDto : class
    {
        Task<IEnumerable<TDto>> GetAll();
        Task<TDto> Get(Guid id);
        Task<TDto> Create(TInputDto dto);
        Task<bool> Update(Guid id, TInputDto dto);
        Task<bool> Delete(Guid id);
    }
}