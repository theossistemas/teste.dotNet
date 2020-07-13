using LibraryStore.Core.Mapper.DtoToEntity.Inputs;
using LibraryStore.Tests.Mocks.Dtos.Inputs;
using LibraryStore.Tests.Mocks.Entities;
using Xunit;

namespace LibraryStore.Tests.Mappers.DtoToEntity.Inputs
{
    public class UserInputsDtoToEntityMapperTests
    {
        private readonly IUserInputDtoToEntityMapper mapper;

        public UserInputsDtoToEntityMapperTests()
        {
            mapper = new UserInputDtoToEntityMapper();
        }

        [Fact(DisplayName = "[UserInputDtoToEntityMapper.ToMap] Deve retornar null quando informado null.")]
        public void DeveRetornarNullQuandoForNull()
        {
            var actual = mapper.ToMap(null);

            Assert.Null(actual);
        }

        [Fact(DisplayName = "[UserInputDtoToEntityMapper.ToMap] Deve retornar entidade quando informado dto preenchido.")]
        public void DeveRetornarPreenchidoQuandoPreenchido()
        {
            var dto = UserInputDtoMock.CreateAdmin();
            var expected = UserMock.CreateInputAdmin();
            var actual = mapper.ToMap(dto);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Fullname, actual.Fullname);
            Assert.Equal(expected.Username, actual.Username);
            Assert.Equal(expected.Password, actual.Password);
            Assert.Equal(expected.Active, actual.Active);
            Assert.Equal(expected.CreatedAt, actual.CreatedAt);
            Assert.Equal(expected.UpdatedAt, actual.UpdatedAt);
        }
    }
}