using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Teste.App.viewModel;

namespace Teste.App.Services
{
    public class ContractPersonApp
    {
        private IRepository<Person> _rep;

        public ContractPersonApp(IRepository<Person> rep)
        {
            _rep = rep;
        }

        public virtual PersonViewModel GetById(int id)
        {
            try
            {
                var entity = _rep.Get(id);

                PersonViewModel person = new PersonViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Cpf = entity.Cpf,
                    BirthDate = entity.BirthDate,
                    GenderId = entity.GenderId
                };
                return person;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual Person GetByName(string name)
        {
            return _rep.GetAll(null).Where(x => x.Name == name).FirstOrDefault();
        }

        public virtual PersonViewModel SavePerson(PersonViewModel modelView)
        {
            try
            {
                Person entity = new Person
                {
                    Name = modelView.Name,
                    Cpf = modelView.Cpf,
                    BirthDate = modelView.BirthDate,
                    GenderId = modelView.GenderId,
                    Email = modelView.Email
                };

                entity = _rep.Insert(entity);

                return new PersonViewModel(entity.Id, entity.Name, entity.BirthDate, entity.Cpf, entity.Email, entity.GenderId);
            }
            catch
            {
                throw new ValidationException();
            }

        }

        public virtual Person EditPerson(PersonViewModel view)
        {
            try
            {
                Person person = _rep.Get(view.Id.Value);

                if (person != null)
                {
                    person.Name = view.Name;
                    person.GenderId = view.GenderId;
                    person.BirthDate = view.BirthDate;
                    _rep.Update(person);
                }

                return person;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePerson(int id)
        {
            Person Person = _rep.Get(id);
            if (Person != null)
            {
                _rep.Delete(Person);
            }
        }

        public List<Person> GetAll(string nome)
        {
            return _rep.GetAll(null).Where(x => (string.IsNullOrWhiteSpace(nome) || x.Name.ToUpper().Contains(nome.ToUpper()))).ToList();
        }
    }
}
