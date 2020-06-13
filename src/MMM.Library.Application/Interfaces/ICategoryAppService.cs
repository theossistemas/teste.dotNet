using MMM.Library.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Application.Interfaces
{
    public interface ICategoryAppService
    {
        Task<CategoryViewModel> GetById(Guid id);
        Task<IEnumerable<CategoryViewModel>> GetAll();

        Task Add(CategoryViewModel categoryViewModel);
        Task Update(CategoryViewModel categoryViewModel);
        Task Delete(Guid id);
    }
}
