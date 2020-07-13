using LibraryStore.Core.Data.Entities;
using LibraryStore.Core.Mapper.DtoToEntity;
using LibraryStore.Tests.Mocks.Dtos;
using LibraryStore.Tests.Mocks.Entities;
using Xunit;

namespace LibraryStore.Tests.Mappers.DtoToEntity
{
    public class UserDtoToEntityMapperTests
    {
        private readonly IUserDtoToEntityMapper mapper;

        public UserDtoToEntityMapperTests()
        {
            mapper = new UserDtoToEntityMapper();
        }

        [Fact(DisplayName = "[UserDtoToEntityMapper.ToMap] Deve retornar null quando informado null.")]
        public void DeveRetornarNuloQuandoForNulo()
        {
            var actual = mapper.ToMap(null);

            Assert.Null(actual);
        }

        [Fact(DisplayName = "[UserDtoToEntityMapper.ToMap] Deve retornar entidade quando informado dto preenchido.")]
        public void DeveRetornarPreenchidoQuandoPreenchido()
        {
            var dto = UserDtoMock.CreateAdmin();
            var expected = UserMock.CreateAdmin();
            var actual = mapper.ToMap(dto);

            AssertEqual(expected, actual);
        }

        [Fact(DisplayName = "[UserDtoToEntityMapper.ToMap] Deve substituir entidade com as informações do dto.")]
        public void DeveRetornarSubstituirQuandoPreenchido()
        {
            var name = "Other name";
            var dto = UserDtoMock.CreateAdmin();
            dto.Fullname = name;
            var expected = UserMock.CreateAdmin();
            expected.Fullname = name;
            var actual = mapper.ToMap(dto, UserMock.CreateAdmin());

            AssertEqual(expected, actual);
        }

        private static void AssertEqual(User expected, User actual)
        { 
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