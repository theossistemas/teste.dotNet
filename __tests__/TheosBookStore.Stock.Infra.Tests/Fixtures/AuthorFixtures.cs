using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Infra.Models;

namespace TheosBookStore.Stock.Infra.Tests.Fixtures
{
    public class AuthorFixtures
    {
        public Author GetAuthorEntity()
        {
            var author = new Author(Faker.Name.FullName());
            author.DefineId(Faker.RandomNumber.Next(100));
            return author;
        }

        public AuthorModel GetAuthorModel()
        {
            var author = new AuthorModel
            {
                Id = Faker.RandomNumber.Next(100),
                Name = Faker.Name.FullName()
            };
            return author;
        }
    }
}
