using System.Collections.Generic;
using FluentValidation;

namespace Livraria.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        TOutputModel Add<TInputModel, TOutputModel,TValidator>(TInputModel inputModel) 
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;
        void Delete(int id);
        IEnumerable<TOutputModel> Get<TOutputModel>() where TOutputModel:class;
        TOutputModel GetById<TOutputModel>(int id) where TOutputModel:class;
        bool VerifyLivro(int id);
        TOutputModel Update<TInputModel, TOutputModel,TValidator>(TInputModel inputModel) 
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;
    }
}