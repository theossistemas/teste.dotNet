using LibraryStore.Core.Mapper.DtoToEntity.Inputs;
using LibraryStore.Tests.Mocks.Dtos.Inputs;
using LibraryStore.Tests.Mocks.Entities;
using Xunit;

namespace LibraryStore.Tests.Mocks.DtoToEntity.Inputs
{
    public class BookInputDtoToEntityMapperTests
    {
        private readonly IBookInputDtoToEntityMapper mapper;

        public BookInputDtoToEntityMapperTests()
        {
            mapper = new BookInputDtoToEntityMapper();
        }

        [Fact(DisplayName = "[BookInputDtoToEntityMapper.ToMap] Deve retornar null quando informado null.")]
        public void DeveRetornarNullQuandoForNull()
        {
            var actual = mapper.ToMap(null);

            Assert.Null(actual);
        }

        [Fact(DisplayName = "[BookInputDtoToEntityMapper.ToMap] Deve retornar entidade quando informado dto preenchido.")]
        public void DeveRetornarPreenchidoQuandoPreenchido()
        {
            var dto = BookInputDtoMock.CreateBook1();
            var expected = BookMock.CreateInputBook1();
            var actual = mapper.ToMap(dto);

            Assert.Equal(actual.Id, expected.Id);
            Assert.Equal(actual.Title, expected.Title);
            Assert.Equal(actual.Description, expected.Description);
            Assert.Equal(actual.UrlImage, expected.UrlImage);
            Assert.Equal(actual.Active, expected.Active);
            Assert.Equal(actual.CreatedAt, expected.CreatedAt);
            Assert.Equal(actual.UpdatedAt, expected.UpdatedAt);
        }
    }
}