using LibraryStore.Core.Data.Dtos;
using LibraryStore.Core.Data.Entities;
using LibraryStore.Core.Mappers;

namespace LibraryStore.Core.Mapper.DtoToEntity
{
    public class BookDtoToEntityMapper : BaseMapper<BookDto, Book>, IBookDtoToEntityMapper
    { }
}