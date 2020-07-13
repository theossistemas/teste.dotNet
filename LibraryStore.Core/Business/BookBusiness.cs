using LibraryStore.Core.Data.Dtos;
using LibraryStore.Core.Data.Entities;
using LibraryStore.Core.DataStorage.Repositories;
using LibraryStore.Core.Mapper.DtoToEntity.Inputs;
using LibraryStore.Core.Mapper.EntityToDto;
using System.Threading.Tasks;

namespace LibraryStore.Core.Business
{
    public class BookBusiness : BaseBusiness<Book, BookDto, BookInputDto>, IBookBusiness
    {
        public BookBusiness(IBookRepository bookRepository,
                            IBookEntityToDtoMapper bookMapper,
                            IBookInputDtoToEntityMapper bookInputDtoMapper)
            : base(bookRepository, bookMapper, bookInputDtoMapper)
        { }

        public override async Task<BookDto> Create(BookInputDto dto)
        {
            if (await ((IBookRepository)repository).ExistsByTitleAsync(dto.Title))
                return null;

            var entity = inputMapper.ToMap(dto);
            entity = await repository.CreateAsync(entity);

            return mapper.ToMap(entity);
        }
    }
}