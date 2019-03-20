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
    public class ContractStateApp
    {
        private IRepository<State> _rep;

        public ContractStateApp(IRepository<State> rep)
        {
            _rep = rep;
        }

        public virtual StateViewModel GetById(int id)
        {
            try
            {
                var entity = _rep.Get(id);

                StateViewModel state = new StateViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Uf = entity.Uf
                };
                return state;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual State GetByName(string name)
        {
            return _rep.GetAll(null).Where(x => x.Name == name).FirstOrDefault();
        }


        public List<StateViewModel> GetAll(string nome)
        {
            List<State> stateList = _rep.GetAll(null).Where(x => string.IsNullOrWhiteSpace(nome) || x.Name.ToUpper().Contains(nome.ToUpper())).ToList();
            List<StateViewModel> stateViewModels = new List<StateViewModel>();
            stateList.ForEach(element =>
            {
                stateViewModels.Add(
                     new StateViewModel { Id = element.Id, Name = element.Name, Uf = element.Uf });
            });

            return stateViewModels;
        }
    }
}
