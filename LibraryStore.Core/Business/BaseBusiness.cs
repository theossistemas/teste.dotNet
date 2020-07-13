using LibraryStore.Core.Data.Dtos;
using LibraryStore.Core.Data.Entities;
using LibraryStore.Core.DataStorage;
using LibraryStore.Core.Mappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryStore.Core.Business
{
    public class BaseBusiness<TEntity, TDto, TInputDto> : IBusiness<TDto, TInputDto>
        where TEntity : class, IEntity
        where TDto : class, IDto
        where TInputDto : class
    {
        protected readonly IRepository<TEntity> repository;
        protected readonly IEntityToDtoMapper<TEntity, TDto> mapper;
        protected readonly IMapper<TInputDto, TEntity> inputMapper;

        public BaseBusiness(IRepository<TEntity> repository,
                            IEntityToDtoMapper<TEntity, TDto> mapper,
                            IMapper<TInputDto, TEntity> inputMapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.inputMapper = inputMapper;
        }

        public async Task<IEnumerable<TDto>> GetAll()
        {
            var list = await repository.FindAllAsync();

            return mapper.ToMapList(list);
        }

        public async Task<TDto> Get(Guid id)
        {
            var entity = await repository.FindAsync(id);

            return mapper.ToMap(entity);
        }

        public virtual async Task<TDto> Create(TInputDto dto)
        {
            var entity = inputMapper.ToMap(dto);

            if (await repository.ExistsAsync(entity))
                return null;

            entity = await repository.CreateAsync(entity);

            return mapper.ToMap(entity);
        }

        public async Task<bool> Update(Guid id, TInputDto dto)
        {
            var entity = await repository.FindAsync(id);

            if (entity == null)
                return false;

            entity = inputMapper.ToMap(dto, entity);

            return await repository.UpdateAsync(entity);
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await repository.FindAsync(id);

            if (entity == null)
                return false;

            return await repository.RemoveAsync(entity);
        }
    }
}
