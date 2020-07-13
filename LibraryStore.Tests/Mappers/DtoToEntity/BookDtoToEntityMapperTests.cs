using LibraryStore.Core.Data.Entities;
using LibraryStore.Core.Mapper.DtoToEntity;
using LibraryStore.Tests.Mocks.Dtos;
using LibraryStore.Tests.Mocks.Entities;
using Xunit;

namespace LibraryStore.Tests.Mappers.DtoToEntity
{
    public class BookDtoToEntityMapperTests
    {
        private readonly IBookDtoToEntityMapper mapper;

        public BookDtoToEntityMapperTests()
        {
            mapper = new BookDtoToEntityMapper();
        }

        [Fact(DisplayName = "[BookDtoToEntityMapper.ToMap] Deve retornar null quando informado null.")]
        public void DeveRetornarNuloQuandoForNulo()
        {
            var actual = mapper.ToMap(null);

            Assert.Null(actual);
        }

        [Fact(DisplayName = "[BookDtoToEntityMapper.ToMap] Deve retornar entidade quando informado dto preenchido.")]
        public void DeveRetornarPreenchidoQuandoPreenchido()
        {
            var dto = BookDtoMock.CreateBook1();
            var expected = BookMock.CreateBook1();
            var actual = mapper.ToMap(dto);

            AssertEqual(expected, actual);
        }

        [Fact(DisplayName = "[UserDtoToEntityMapper.ToMap] Deve substituir entidade com as informações do dto.")]
        public void DeveRetornarSubstituirQuandoPreenchido()
        {
            var title = "Other title";
            var description = "Other description";
            var dto = BookDtoMock.CreateBook1();
            dto.Title = title;
            dto.Description = description;
            var expected = BookMock.CreateBook1();
            expected.Title = title;
            expected.Description = description;
            var actual = mapper.ToMap(dto, BookMock.CreateBook1());

            AssertEqual(expected, actual);
        }

        private static void AssertEqual(Book expected, Book actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.Author, actual.Author);
            Assert.Equal(expected.UrlImage, actual.UrlImage);
            Assert.Equal(expected.Active, actual.Active);
            Assert.Equal(expected.CreatedAt, actual.CreatedAt);
            Assert.Equal(expected.UpdatedAt, actual.UpdatedAt);
        }
    }
}