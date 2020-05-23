using FluentAssertions;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Infra.Models;
using TheosBookStore.Stock.Infra.Tests.Fixtures;
using Xunit;

namespace TheosBookStore.Stock.Infra.Tests.Mapper.Profiles
{
    public class AuthorProfileTest : IClassFixture<AuthorFixtures>,
        IClassFixture<AutoMapperFixture>
    {
        private readonly AuthorFixtures _authorFixtures;

        public AutoMapperFixture _mapper { get; }

        public AuthorProfileTest(AuthorFixtures authorFixtures, AutoMapperFixture autoMapperFixture)
        {
            _authorFixtures = authorFixtures;
            _mapper = autoMapperFixture;
        }
        [Fact]
        public void ShouldMapEntityToModel()
        {
            var entity = _authorFixtures.GetAuthorEntity();

            var model = _mapper.Mapper.Map<AuthorModel>(entity);

            model.Id.Should().Be(entity.Id);
            model.Name.Should().Be(entity.Name);
        }

        [Fact]
        public void ShouldMapModelToEntity()
        {
            var model = _authorFixtures.GetAuthorModel();

            var entity = _mapper.Mapper.Map<Author>(model);

            entity.Id.Should().Be(model.Id);
            entity.Name.Should().Be(model.Name);
        }
    }
}
