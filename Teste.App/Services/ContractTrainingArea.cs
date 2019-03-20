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
    public class ContractTrainingAreaApp
    {
        private IRepository<TrainingArea> _rep;

        public ContractTrainingAreaApp(IRepository<TrainingArea> rep)
        {
            _rep = rep;
        }

        public virtual TrainingAreaViewModel GetById(int id)
        {
            try
            {
                var entity = _rep.Get(id);

                TrainingAreaViewModel TrainingArea = new TrainingAreaViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    College =  entity.College,
                    YearFinish = entity.YearFinish,
                    YearInit = entity.YearInit
                };
                return TrainingArea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual TrainingArea GetByName(string name)
        {
            return _rep.GetAll(null).Where(x => x.Name == name).FirstOrDefault();
        }

        public virtual TrainingArea SaveTrainingArea(TrainingAreaViewModel modelView)
        {
            try
            {
                TrainingArea entity = new TrainingArea
                {
                    Name = modelView.Name,
                    College = modelView.College,
                    YearFinish = modelView.YearFinish,
                    YearInit = modelView.YearInit
                };
                _rep.Insert(entity);

                return entity;
            }
            catch
            {
                throw new ValidationException();
            }

        }

        public virtual TrainingArea EditTrainingArea(TrainingAreaViewModel modelView)
        {
            try
            {
                TrainingArea TrainingArea = _rep.Get(modelView.Id.Value);

                if (TrainingArea != null)
                {
                    TrainingArea.Name = modelView.Name;
                    TrainingArea.College = modelView.College;
                    TrainingArea.YearFinish = modelView.YearFinish;
                    TrainingArea.YearInit = modelView.YearInit;
                    _rep.Update(TrainingArea);
                }

                return TrainingArea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteTrainingArea(int id)
        {
            TrainingArea TrainingArea = _rep.Get(id);
            if (TrainingArea != null)
            {
                _rep.Delete(TrainingArea);
            }
        }

        public List<TrainingArea> GetAll(string nome)
        {
            return _rep.GetAll(null).Where(x => (string.IsNullOrWhiteSpace(nome) || x.Name.ToUpper().Contains(nome.ToUpper()))).ToList();
        }
    }
}
