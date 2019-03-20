using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Teste.App.viewModel;
using Teste.Domain.Repositories;

namespace Teste.App.Services
{
    public class ContractAddressApp
    {
        private readonly IAddressRepository _repository;

        public ContractAddressApp(IAddressRepository repository)
        {
            _repository = repository;
        }

        public virtual AddressViewModel GetById(int id)
        {
            try
            {
                var entity = _repository.Get(id);

                AddressViewModel Address = new AddressViewModel
                {
                    Id = entity.Id,
                    Street = entity.Street,
                    Number = entity.Number,
                    Cep = entity.Cep,
                    CityId = entity.CityId,
                    Complement = entity.Complement,
                    Neighborhood = entity.Neighborhood,
                    PersonId = entity.PersonId
                };
                return Address;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual Address GetByName(string name)
        {
            return _repository.GetAll(null).Where(x => x.Street == name).FirstOrDefault();
        }

        public virtual Address SaveAddress(AddressViewModel modelView)
        {
            try
            {
                Address entity = new Address
                {

                    Street = modelView.Street,
                    Number = modelView.Number,
                    Cep = modelView.Cep,
                    CityId = modelView.CityId,
                    Complement = modelView.Complement,
                    Neighborhood = modelView.Neighborhood,
                    PersonId = modelView.PersonId
                };
                _repository.Insert(entity);

                return entity;
            }
            catch
            {
                throw new ValidationException();
            }

        }

        public virtual Address EditAddress(AddressViewModel modelView)
        {
            try
            {
                Address Address = _repository.Get(modelView.Id.Value);

                if (Address != null)
                {
                    Address.Street = modelView.Street;
                    Address.Number = modelView.Number;
                    Address.Cep = modelView.Cep;
                    Address.CityId = modelView.CityId;
                    Address.Complement = modelView.Complement;
                    Address.Neighborhood = modelView.Neighborhood;
                    Address.PersonId = modelView.PersonId;
                    _repository.Update(Address);
                }

                return Address;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAddress(int id)
        {
            Address Address = _repository.Get(id);
            if (Address != null)
            {
                _repository.Delete(Address);
            }
        }

        public List<Address> GetAll(string nome)
        {
            return _repository.GetAll(null).Where(x => (string.IsNullOrWhiteSpace(nome) || x.Street.ToUpper().Contains(nome.ToUpper()))).ToList();
        }

        public Address GetAddressByPersonId(int personId)
        {
            return _repository.GetAddressByPersonId(personId);
        }
    }
}
