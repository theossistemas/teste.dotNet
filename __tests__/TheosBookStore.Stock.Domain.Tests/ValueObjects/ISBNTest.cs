using FluentAssertions;
using TheosBookStore.Stock.Domain.ValueObjects;
using Xunit;

namespace TheosBookStore.Stock.Domain.Tests.ValueObjects
{
    public class ISBNTest
    {
        private const string VALID_ISBN = "978011000222";
        private const string INVALID_ISBN_SMALL = "12345678901";
        private const string INVALID_ISBN_BIGGER = "1234567890123";
        private const string INVALID_ISBN_LETTERS = "12345678901a";

        [Fact]
        public void ShouldCreateISBN()
        {
            var isbn = new ISBN(VALID_ISBN);

            isbn.Should().NotBeNull();
            isbn.Value.Should().Be(VALID_ISBN);
        }

        [Fact]
        public void ShouldBeInvalidWhenHaveLessThan12Digits()
        {
            var isbn = new ISBN(INVALID_ISBN_SMALL);

            var validISBN = isbn.IsValid();

            validISBN.Should().BeFalse();
        }

        [Fact]
        public void ShouldBeInvalidWhenBiggerThan12Digits()
        {
            var isbn = new ISBN(INVALID_ISBN_BIGGER);

            var validISBN = isbn.IsValid();

            validISBN.Should().BeFalse();
        }

        [Fact]
        public void ShouldHaveOnlyDigits()
        {
            var isbn = new ISBN(INVALID_ISBN_LETTERS);

            var validISBN = isbn.IsValid();

            validISBN.Should().BeFalse();
        }
    }
}
