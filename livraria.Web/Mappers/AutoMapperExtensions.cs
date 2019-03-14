using AutoMapper;
using System.Collections.Generic;

namespace livraria.Web.Mappers
{
    public static class AutoMapperExtensions
    {
        public static T MapTo<T>(this object value)
        {
            return Mapper.Map<T>(value);
        }

        public static IList<T> IListTo<T>(this object value)
        {
            return Mapper.Map<IList<T>>(value);
        }
    }
}
