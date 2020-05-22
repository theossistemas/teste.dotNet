using FluentAssertions;
using TheosBookStore.Stock.Domain.Entities;
using Xunit;

namespace TheosBookStore.Stock.Domain.Tests.Entities
{
    public class AuthorTest
    {
        private const string AUTHOR_NAME = "Author name";
        [Fact]
        public void ShouldCreateAuthor()
        {
            var author = new Author("Author name");

            author.Should().NotBeNull();
            author.Name.Should().Be(AUTHOR_NAME);
        }
    }
}
