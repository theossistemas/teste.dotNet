using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Theos.Library.Core.Data.Context;
using Theos.Library.Core.Data.Repository.Interface.Base;
using Theos.Library.CrossCutting;
using Theos.Library.CrossCutting.Exceptions;
using Theos.Library.CrossCutting.Filter.Base;
using Theos.Library.CrossCutting.Request;
using Theos.Library.CrossCutting.Response;
using Theos.Library.Domain.Base;
using Theos.Library.Domain.Logs;

namespace Theos.Library.Core.Data.Repository.Base
{
    public class BaseRepository<T, TT, TK> : IBaseRepository<T, TT> where T : BaseRelationShip<TK> where TT : BaseFilter where TK : BaseKey
    {
        public virtual DataContext GetContext()
        {
            return new DataContext();
        }

        public Guid? GetIdByKey(Guid key)
        {
            using (var context = GetContext())
            {
                return GetIdByKey(key, context);
            }
        }

        public Guid? GetIdByKey(Guid key, DataContext context)
        {
            return context.Set<T>().FirstOrDefault(f => f.KeyId == key)?.Id;
        }

        public virtual Guid Create(T dto, DataContext context)
        {
            dto.Date = DateTime.Now;
            dto.Active = BeginsActive();
            dto.Logs = new List<Log>();
            dto.Version = 1;

            context.Set<T>().Add(dto);
            SetRelationship(dto);
            context.SaveChanges();

            CreateLog(dto, context);
            context.SaveChanges();
            return dto.Id;
        }

        protected virtual void SetRelationship(T dto)
        {
        }

        public virtual Guid Create(T dto)
        {
            using (var context = GetContext())
            {
                return Create(dto, context);
            }
        }

        protected virtual bool BeginsActive()
        {
            return true;
        }

        public virtual T Update(T dto)
        {
            using (var context = GetContext())
            {
                return Update(dto, context);
            }
        }

        public virtual T Update(T dto, DataContext context)
        {
            var currentValue = SetValue(dto, context);
            context.SaveChanges();

            CreateLog(currentValue, context);
            context.SaveChanges();
            return ExtractFromContext(currentValue);
        }

        private T SetValue(T obj, DataContext context)
        {
            var id = obj.GetPk();
            var currentValue = context.Set<T>().Find(id);
            if (currentValue?.Version != obj.Version)
                throw new VersionException("Record outdated, update and try again");

            SetValue(context, obj, currentValue);

            currentValue.Date = DateTime.Now;
            currentValue.Logs = new List<Log>();

            return currentValue;
        }

        protected virtual void SetValue(DataContext context, T obj, T currentValue)
        {
            var key = currentValue.KeyId;
            obj.Version++;
            context.Entry(currentValue).CurrentValues.SetValues(obj);

            currentValue.KeyId = key;
        }

        public virtual T FindByKey(Guid key)
        {
            using (var context = GetContext())
            {
                return FindByKey(key, context);
            }
        }

        public virtual T FindByKey(Guid key, DataContext context)
        {
            var entity = context.Set<T>().FirstOrDefault(f => f.KeyId == key);
            return ExtractFromContext(entity);
        }

        public virtual void Remove(Guid id)
        {
            using (var context = GetContext())
            {
                Remove(id, context);
            }
        }

        public virtual void Remove(Guid id, DataContext context)
        {
            var obj = Find(id, context);
            if (obj == null)
                throw new NotFoundException();

            var currentValue = SetValue(obj, context);
            currentValue.Active = false;
            
            context.SaveChanges();

            CreateLog(currentValue, context);
            context.SaveChanges();
        }

        public virtual T Find(Guid id)
        {
            using (var context = GetContext())
            {
                return Find(id, context);
            }
        }

        public virtual T Find(Guid id, DataContext context)
        {
            return ExtractFromContext(context.Set<T>().FirstOrDefault(f => f.Active && f.Id == id));
        }

        public virtual List<T> All()
        {
            using (var context = GetContext())
            {
                var response = context.Set<T>().Where(w => w.Active).Take(EnvironmentProperties.RequestLimit).ToList();
                return ExtractFromContext(response);
            }
        }

        public virtual ResponseModel<T> Search(RequestModel<TT> request)
        {
            using (var context = GetContext())
            {
                return Search(request, context);
            }
        }

        public virtual ResponseModel<T> Search(RequestModel<TT> request, DataContext context)
        {
            var query = context.Set<T>().AsQueryable();
            query = Filter(request, query, context);

            var total = query.Select(s => 1).Count();
            var skip = request.Page > 1 ? (request.Page - 1) * request.PerPage : 0;
            var temp = query.Skip(skip).Take(request.PerPage);

            return new ResponseModel<T>
            {
                CurrentPage = request.Page,
                Data = ExtractFromContext(temp.ToList()),
                PerPage = request.PerPage,
                Total = total
            };
        }

        protected virtual IQueryable<T> Filter(RequestModel<TT> request, IQueryable<T> query, DataContext context)
        {
            return query.Where(w => w.Active);
        }

        private void CreateLog(object obj, DataContext context)
        {
            CreateLog(obj, new List<string>(), context);
        }

        private void CreateLog(object obj, ICollection<string> origin, DataContext context)
        {
            var baseEntity = obj as BaseEntity;
            var table = baseEntity?.GetType().Name;
            var date = DateTime.Now;

            baseEntity?.Logs?.Where(w => !string.IsNullOrEmpty(w.Value)).ToList().ForEach(log =>
            {
                log.OriginId = baseEntity.GetPk();
                log.Version = baseEntity.Version;
                log.Date = date;
                log.Table = table;
                log.Active = true;

                context.Set<Log>().Add(log);
                context.SaveChanges();

                baseEntity.Logs = new List<Log>();

                baseEntity.GetType()?.GetProperties()?.ToList().ForEach(property =>
                {
                    var list = GetList(baseEntity, property.Name, origin);
                    if (list.Count == 0)
                        return;

                    origin.Add(table);

                    foreach (var item in list)
                    {
                        CreateLog(item as BaseEntity, origin, context);
                    }
                });
            });
        }

        private static IList GetList(BaseEntity baseEntity, string property, ICollection<string> origin)
        {
            var field = baseEntity.GetType().GetProperty(property);

            if (field == null || !field.PropertyType.Name.Contains(typeof(ICollection).Name))
                return new List<object>();

            if (!(field.GetValue(baseEntity) is IList list))
                return new List<object>();

            if (list.Count == 0)
                return new List<object>();

            var propertyName = list[0]?.GetType().Name;

            return origin.Contains(propertyName) ? new List<object>() : list;
        }

        protected TE ExtractFromContext<TE>(TE dto)
        {
            var temp = JsonConvert.SerializeObject(dto);
            return JsonConvert.DeserializeObject<TE>(temp);
        }

        protected List<TE> ExtractFromContext<TE>(List<TE> dto)
        {
            var temp = JsonConvert.SerializeObject(dto);
            return JsonConvert.DeserializeObject<List<TE>>(temp);
        }

        protected List<TE> ExtractFromContext<TE>(IEnumerable<TE> dto)
        {
            return ExtractFromContext(dto.ToList());
        }
    }
}
