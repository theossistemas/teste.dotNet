using System;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Infra.Models;

namespace TheosBookStore.Stock.Infra.Tests.Fixtures
{
    public class PublisherFixtures
    {
        internal Publisher GetEntity()
        {
            var entity = new Publisher(Faker.Name.FullName());
            entity.DefineId(Faker.RandomNumber.Next(100));
            return entity;
        }

        internal PublisherModel GetModel()
        {
            var model = new PublisherModel
            {
                Id = Faker.RandomNumber.Next(100),
                Name = Faker.Name.FullName()
            };
            return model;
        }
    }
}
