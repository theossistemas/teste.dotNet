using FluentAssertions;
using TheosBookStore.Stock.Domain.Entities;
using Xunit;

namespace TheosBookStore.Stock.Domain.Tests.Entities
{
    public class PublisherTest
    {
        private const string PUBLISHER_NAME = "Publisher name";

        [Fact]
        public void ShouldCreate()
        {
            var publisher = new Publisher(PUBLISHER_NAME);

            publisher.Should().NotBeNull();
            publisher.Name.Should().Be(PUBLISHER_NAME);
        }

        [Fact]
        public void ShouldBeInvalidWhenNameIsNull()
        {
            var publisher = new Publisher(string.Empty);

            var valid = publisher.IsValid();

            valid.Should().BeFalse();
        }
    }
}
