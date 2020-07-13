using LibraryStore.Core.Data.Dtos;
using LibraryStore.Core.Data.Entities;

namespace LibraryStore.Core.Mappers
{
    public interface IDtoToEntityMapper<TParam, TResult> : IMapper<TParam, TResult>
        where TParam : class, IDto
        where TResult : class, IEntity
    { }
}