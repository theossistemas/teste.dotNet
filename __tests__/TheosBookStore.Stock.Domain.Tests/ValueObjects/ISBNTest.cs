using FluentAssertions;
using TheosBookStore.Stock.Domain.ValueObjects;
using Xunit;

namespace TheosBookStore.Stock.Domain.Tests.ValueObjects
{
    public class ISBNTest
    {
        private const string VALID_ISBN = "978011000222";
        [Fact]
        public void ShouldCreateISBN()
        {
            var isbn = new ISBN(VALID_ISBN);

            isbn.Should().NotBeNull();
            isbn.Value.Should().Be(VALID_ISBN);
        }
    }
}
