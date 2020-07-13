using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace LibraryStore.Core.Mappers
{
    public abstract class BaseMapper<TParam, TResult> : IMapper<TParam, TResult>
        where TParam : class
        where TResult : class
    {
        private readonly AutoMapper.IMapper mapper;

        protected BaseMapper()
        {
            mapper = new MapperConfiguration(cfg => CreateMap(cfg)).CreateMapper();
        }

        protected virtual IMappingExpression<TParam, TResult> CreateMap(IMapperConfigurationExpression cfg)
        {
            return cfg.CreateMap<TParam, TResult>();
        }

        public TResult ToMap(TParam obj)
        {
            return mapper.Map<TResult>(obj);
        }

        public TResult ToMap(TParam obj, TResult result)
        {
            return mapper.Map(obj, result);
        }

        public IList<TResult> ToMapList<T>(T list)
            where T : IList<TParam>, ICollection<TParam>
        {
            return list.Select(itm => ToMap(itm)).ToList();
        }
    }
}
