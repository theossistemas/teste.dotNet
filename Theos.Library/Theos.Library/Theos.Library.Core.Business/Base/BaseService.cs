using System;
using System.Collections.Generic;
using Theos.Library.Core.Business.Interface.Base;
using Theos.Library.Core.Data.Context;
using Theos.Library.Core.Data.Repository.Interface.Base;
using Theos.Library.CrossCutting.Filter.Base;
using Theos.Library.CrossCutting.Request;
using Theos.Library.CrossCutting.Response;

namespace Theos.Library.Core.Business.Base
{
    public class BaseService<T, TT> : IBaseService<T, TT> where T : class where TT : BaseFilter
    {
        protected readonly IBaseRepository<T, TT> Repository;

        public BaseService(IBaseRepository<T, TT> repository)
        {
            Repository = repository;
        }

        public virtual Guid Create(T dto)
        {
            return Repository.Create(dto);
        }

        public virtual Guid Create(T dto, DataContext context)
        {
            return Repository.Create(dto, context);
        }

        public virtual T Update(T dto)
        {
            return Repository.Update(dto);
        }

        public T Update(T dto, DataContext context)
        {
            return Repository.Update(dto, context);
        }

        public virtual void Remove(Guid id)
        {
            Repository.Remove(id);
        }

        public void Remove(Guid id, DataContext context)
        {
            Repository.Remove(id, context);
        }

        public virtual T Find(Guid id)
        {
            return Repository.Find(id);
        }

        public T Find(Guid id, DataContext context)
        {
            return Repository.Find(id, context);
        }

        public virtual List<T> All()
        {
            return Repository.All();
        }

        public virtual ResponseModel<T> Search(RequestModel<TT> request)
        {
            return Repository.Search(request);
        }

        public ResponseModel<T> Search(RequestModel<TT> request, DataContext context)
        {
            return Repository.Search(request, context);
        }

        public DataContext GetContext()
        {
            return Repository.GetContext();
        }

        public Guid? GetIdByKey(Guid key, DataContext context)
        {
            return Repository.GetIdByKey(key, context);
        }

        public Guid? GetIdByKey(Guid key)
        {
            return Repository.GetIdByKey(key);
        }

        public T FindByKey(Guid key)
        {
            return Repository.FindByKey(key);
        }

        public T FindByKey(Guid key, DataContext context)
        {
            return Repository.FindByKey(key, context);
        }
    }
}
