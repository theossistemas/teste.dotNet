using LibraryStore.Core.Data.Dtos;
using LibraryStore.Core.Data.Entities;

namespace LibraryStore.Core.Mappers
{
    public interface IEntityToDtoMapper<TParam, TResult> : IMapper<TParam, TResult>
        where TParam : class, IEntity
        where TResult : class, IDto
    { }
}