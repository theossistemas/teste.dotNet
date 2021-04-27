using System.Collections.Generic;
using Livraria.Domain.Security.Entities;
using FluentValidation;

namespace Livraria.Domain.Security.Interfaces
{
    public interface ISecurityBaseService<TEntity> where TEntity : class
    {
        TOutputModel Add<TInputModel, TOutputModel,TValidator>(TInputModel inputModel) 
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;
        void Delete(int id);
        IEnumerable<TOutputModel> Get<TOutputModel>() where TOutputModel:class;
        TOutputModel GetById<TOutputModel>(int id) where TOutputModel:class;
        // TOutputModel GetByLoginAndPassword<TInputModel,TOutputModel>(string login, string password) 
        // where TInputModel:class
        // where TOutputModel:class;
        // bool VerifyUser(int id);
        TOutputModel Update<TInputModel, TOutputModel,TValidator>(TInputModel inputModel) 
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;
    }
}