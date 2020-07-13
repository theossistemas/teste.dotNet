using LibraryStore.Core.Data.Dtos;
using LibraryStore.Core.Data.Entities;
using LibraryStore.Core.Mapper.EntityToDto;
using LibraryStore.Tests.Mocks.Dtos;
using LibraryStore.Tests.Mocks.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryStore.Tests.Mappers.EntityToDto
{
    public class BookEntityToDtoMapperTests
    {
        private readonly IBookEntityToDtoMapper mapper;

        public BookEntityToDtoMapperTests()
        {
            mapper = new BookEntityToDtoMapper();
        }

        [Fact(DisplayName = "[BookEntityToDtoMapper.ToMap] Deve retornar null quando informado null.")]
        public void DeveRetornarNuloQuandoForNulo()
        {
            var actual = mapper.ToMap(null);

            Assert.Null(actual);
        }

        [Fact(DisplayName = "[BookEntityToDtoMapper.ToMap] Deve retornar entidade quando informado dto preenchido.")]
        public void DeveRetornarPreenchidoQuandoPreenchido()
        {
            var entity = BookMock.CreateBook1();
            var expected = BookDtoMock.CreateBook1();
            var actual = mapper.ToMap(entity);

            AssertEqual(expected, actual);
        }

        [Fact(DisplayName = "[BookEntityToDtoMapper.ToMap] Deve retornar lista de entidades quando informado lista de dtos preenchidas.")]
        public void DeveRetornarListaPreenchidaQuandoPreenchida()
        {
            var entity = BookMock.CreateBook1();
            var expected = BookDtoMock.CreateBook1();
            var actual = mapper.ToMapList(new List<Book> { entity });

            AssertEqual(expected, actual.FirstOrDefault());
        }

        private static void AssertEqual(BookDto expected, BookDto actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.Author, actual.Author);
            Assert.Equal(expected.UrlImage, actual.UrlImage);
            Assert.Equal(expected.CreatedAt, actual.CreatedAt);
            Assert.Equal(expected.UpdatedAt, actual.UpdatedAt);
            Assert.Equal(expected.Active, actual.Active);
        }
    }
}