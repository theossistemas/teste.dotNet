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
    public class ContractProfessionApp
    {
        private IRepository<Profession> _rep;

        public ContractProfessionApp(IRepository<Profession> rep)
        {
            _rep = rep;
        }

        public virtual ProfessionViewModel GetById(int id)
        {
            try
            {
                var entity = _rep.Get(id);

                ProfessionViewModel person = new ProfessionViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                };
                return person;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual Profession GetByName(string name)
        {
            return _rep.GetAll(null).Where(x => x.Name == name).FirstOrDefault();
        }

        public virtual Profession SaveProfession(ProfessionViewModel modelView)
        {
            try
            {
                Profession entity = new Profession
                {
                    Name = modelView.Name,
                };
                _rep.Insert(entity);

                return entity;
            }
            catch
            {
                throw new ValidationException();
            }

        }

        public virtual Profession EditProfession(ProfessionViewModel view)
        {
            try
            {
                Profession profession = _rep.Get(view.Id.Value);

                if (profession != null)
                {
                    profession.Name = view.Name;
                    _rep.Update(profession);
                }

                return profession;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteProfession(int id)
        {
            Profession Person = _rep.Get(id);
            if (Person != null)
            {
                _rep.Delete(Person);
            }
        }

        public List<Profession> GetAll(string nome)
        {
            return _rep.GetAll(null).Where(x => (string.IsNullOrWhiteSpace(nome) || x.Name.ToUpper().Contains(nome.ToUpper()))).ToList();
        }
    }
}
