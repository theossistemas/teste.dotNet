using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teste.Domain.Repositories;
using Teste.Impl.Context;

namespace Teste.Impl.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly DataContext _dbContext;
        public AddressRepository(DataContext dbContext)
       : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Address GetAddressByPersonId(int personId)
        {
            var address = _dbContext.Set<Address>().AsQueryable();
            var state = _dbContext.Set<State>().AsQueryable();
            var city = _dbContext.Set<City>().AsQueryable();

            var query = from x in address
                        from j in city.Where(c => c.Id == x.CityId).DefaultIfEmpty()
                        from i in state.Where(e => e.Id == x.City.StateId).DefaultIfEmpty()
                        where x.PersonId == personId
                        select new Address
                        {
                            Street = x.Street,
                            Cep = x.Cep,
                            CityId = x.CityId,
                            City = j == null ? null : new City
                            {
                                Id = j.Id,
                                State = i == null ? null : new State
                                {
                                    Id = i.Id,
                                    Name = i.Name,
                                    Uf = i.Uf
                                },
                               Name = j.Name,
                               StateId = j.StateId
                            },
                            Complement = x.Complement,
                            Id = x.Id,
                            Neighborhood = x.Neighborhood ,
                            Number = x.Number,
                            PersonId = x.PersonId
                        };

            return query.FirstOrDefault();
        }
    }
}
