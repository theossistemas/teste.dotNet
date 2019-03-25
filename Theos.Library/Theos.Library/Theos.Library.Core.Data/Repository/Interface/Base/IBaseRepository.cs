using System;
using System.Collections.Generic;
using Theos.Library.Core.Data.Context;
using Theos.Library.CrossCutting.Filter.Base;
using Theos.Library.CrossCutting.Request;
using Theos.Library.CrossCutting.Response;

namespace Theos.Library.Core.Data.Repository.Interface.Base
{
    public interface IBaseRepository<T, TT> where T : class where TT : BaseFilter
    {
        Guid Create(T dto);
        Guid Create(T dto, DataContext context);
        T Update(T dto);
        T Update(T dto, DataContext context);
        void Remove(Guid id);
        void Remove(Guid id, DataContext context);
        T Find(Guid id);
        T Find(Guid id, DataContext context);
        T FindByKey(Guid key);
        T FindByKey(Guid key, DataContext context);
        List<T> All();
        ResponseModel<T> Search(RequestModel<TT> request);
        ResponseModel<T> Search(RequestModel<TT> request, DataContext context);
        DataContext GetContext();
        Guid? GetIdByKey(Guid key);
        Guid? GetIdByKey(Guid key, DataContext context);
    }
}
