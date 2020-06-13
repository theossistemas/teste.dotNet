using AutoMapper;
using MMM.Library.Application.Interfaces;
using MMM.Library.Application.ViewModels;
using MMM.Library.Domain.Interfaces;
using MMM.Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryAppService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task Add(CategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            _categoryRepository.Add(category);

            await _categoryRepository.UnitOfWork.Commit();
        }

        public async Task Update(CategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            _categoryRepository.Update(category);

            await _categoryRepository.UnitOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            _categoryRepository.Delete(id);

            await _categoryRepository.UnitOfWork.Commit();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.GetAll());
        }

        public async Task<CategoryViewModel> GetById(Guid id)
        {
            return _mapper.Map<CategoryViewModel>(await _categoryRepository.GetById(id));
        }

       
    }
}
