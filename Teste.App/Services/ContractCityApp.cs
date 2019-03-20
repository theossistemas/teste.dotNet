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
    public class ContractCityApp
    {
        private readonly ICityRepository _repository;
        public ContractCityApp(ICityRepository repository)
        {
            _repository = repository;
        }

        public virtual CityViewModel GetById(int id)
        {
            try
            {
                var entity = _repository.Get(id);

                CityViewModel city = new CityViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    
                };
                return city;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual City GetByName(string name)
        {
            return _repository.GetAll(null).Where(x => x.Name == name).FirstOrDefault();
        }

        public List<City> GetAll(string nome)
        {
            return _repository.GetAll(null).Where(x => (string.IsNullOrWhiteSpace(nome) || x.Name.ToUpper().Contains(nome.ToUpper()))).ToList();
        }

        public List<City> GetAllCitiesByStateId(int stateId)
        {
            return _repository.GetCityByStateId(stateId).ToList();
        }
    }
}
