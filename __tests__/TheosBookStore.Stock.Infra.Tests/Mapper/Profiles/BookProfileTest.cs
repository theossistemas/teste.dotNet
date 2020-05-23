using System.Linq;
using FluentAssertions;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Infra.Models;
using TheosBookStore.Stock.Infra.Tests.Fixtures;
using Xunit;

namespace TheosBookStore.Stock.Infra.Tests.Mapper.Profiles
{
    public class BookProfileTest : IClassFixture<BookFixtures>,
        IClassFixture<AutoMapperFixture>
    {
        private readonly BookFixtures _bookFixtures;
        private readonly AutoMapperFixture _mapper;

        public BookProfileTest(BookFixtures bookFixtures, AutoMapperFixture autoMapperFixture)
        {
            _bookFixtures = bookFixtures;
            _mapper = autoMapperFixture;
        }
        [Fact]
        public void ShouldMapEntityToModel()
        {
            Book entity = _bookFixtures.GetEntity();

            BookModel model = _mapper.Mapper.Map<BookModel>(entity);

            model.Id.Should().Be(entity.Id);
            model.Title.Should().Be(entity.Title);
            model.ISBN.Should().Be(entity.ISBN.Value);
            model.Authors.Should().HaveSameCount(entity.Authors);
            model.Authors.Should()
                .Equal(entity.Authors,
                    (bookAuthor, authorEntity) =>
                        bookAuthor.AuthorId == authorEntity.Id
                        && bookAuthor.Author.Id == authorEntity.Id
                        && bookAuthor.Author.Name == authorEntity.Name
                );
            model.PageCount.Should().Be(entity.PageCount);
            model.Publisher.Id.Should().Be(entity.Publisher.Id);
            model.Publisher.Name.Should().Be(entity.Publisher.Name);
            model.Year.Should().Be(entity.YearPublication);
            model.Edition.Should().Be(entity.Edition);
        }

        [Fact]
        public void ShouldMapModelToEntity()
        {
            BookModel model = _bookFixtures.GetModel();

            Book entity = _mapper.Mapper.Map<Book>(model);

            entity.Id.Should().Be(model.Id);
            entity.Title.Should().Be(model.Title);
            entity.ISBN.Value.Should().Be(model.ISBN);
            entity.Authors.Should().HaveSameCount(model.Authors);
            entity.Authors.Should()
                .Equal(model.Authors,
                    (authorEntity, bookAuthor) =>
                        authorEntity.Id == bookAuthor.Author.Id
                        && authorEntity.Id == bookAuthor.Author.Id
                        && authorEntity.Name == bookAuthor.Author.Name
                        && authorEntity.Name == bookAuthor.Author.Name
                );
            entity.PageCount.Should().Be(model.PageCount);
            entity.Publisher.Id.Should().Be(model.Publisher.Id);
            entity.Publisher.Name.Should().Be(model.Publisher.Name);
            entity.YearPublication.Should().Be(model.Year);
            entity.Edition.Should().Be(model.Edition);
        }
    }
}
