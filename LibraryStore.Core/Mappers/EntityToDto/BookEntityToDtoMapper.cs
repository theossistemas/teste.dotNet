using LibraryStore.Core.Data.Dtos;
using LibraryStore.Core.Data.Entities;
using LibraryStore.Core.Mappers;

namespace LibraryStore.Core.Mapper.EntityToDto
{
    public class BookEntityToDtoMapper : BaseMapper<Book, BookDto>, IBookEntityToDtoMapper
    { }
}
