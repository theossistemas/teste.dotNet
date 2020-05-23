using FluentAssertions;

using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Infra.Models;
using TheosBookStore.Stock.Infra.Tests.Fixtures;

using Xunit;

namespace TheosBookStore.Stock.Infra.Tests.Mapper.Profiles
{
    public class PublisherProfileTest : IClassFixture<PublisherFixtures>,
        IClassFixture<AutoMapperFixture>
    {
        private readonly PublisherFixtures _publisherFixtures;
        private readonly AutoMapperFixture _mapper;

        public PublisherProfileTest(PublisherFixtures publisherFixtures,
            AutoMapperFixture autoMapperFixture)
        {
            _publisherFixtures = publisherFixtures;
            _mapper = autoMapperFixture;
        }

        [Fact]
        public void ShouldMapEntityToModel()
        {
            Publisher entity = _publisherFixtures.GetEntity();

            PublisherModel model = _mapper.Mapper.Map<PublisherModel>(entity);

            model.Id.Should().Be(entity.Id);
            model.Name.Should().Be(entity.Name);
        }

        [Fact]
        public void ShouldMapModelToEntity()
        {
            PublisherModel model = _publisherFixtures.GetModel();

            Publisher entity = _mapper.Mapper.Map<Publisher>(model);

            entity.Id.Should().Be(model.Id);
            entity.Name.Should().Be(model.Name);
        }
    }
}
