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
    public class UserEntityToDtoMapperTests
    {
        private readonly IUserEntityToDtoMapper mapper;

        public UserEntityToDtoMapperTests()
        {
            mapper = new UserEntityToDtoMapper();
        }

        [Fact(DisplayName = "[UserEntityToDtoMapper.ToMap] Deve retornar null quando informado null.")]
        public void DeveRetornarNuloQuandoForNulo()
        {
            var actual = mapper.ToMap(null);

            Assert.Null(actual);
        }

        [Fact(DisplayName = "[UserEntityToDtoMapper.ToMap] Deve retornar entidade quando informado dto preenchido.")]
        public void DeveRetornarPreenchidoQuandoPreenchido()
        {
            var entity = UserMock.CreateAdmin();
            var expected = UserDtoMock.CreateAdmin();
            var actual = mapper.ToMap(entity);

            AssertEqual(expected, actual);
        }

        [Fact(DisplayName = "[UserEntityToDtoMapper.ToMap] Deve retornar lista de entidades quando informado lista de dtos preenchidas.")]
        public void DeveRetornarListaPreenchidaQuandoPreenchida()
        {
            var entity = UserMock.CreateAdmin();
            var expected = UserDtoMock.CreateAdmin();
            var actual = mapper.ToMapList(new List<User> { entity });

            AssertEqual(expected, actual.FirstOrDefault());
        }

        private static void AssertEqual(UserDto expected, UserDto actual)
        {
            Assert.Equal(actual.Id, expected.Id);
            Assert.Equal(actual.Fullname, expected.Fullname);
            Assert.Equal(actual.Username, expected.Username);
            Assert.Equal(actual.CreatedAt, expected.CreatedAt);
            Assert.Equal(actual.UpdatedAt, expected.UpdatedAt);
            Assert.Equal(actual.Active, expected.Active);
        }
    }
}