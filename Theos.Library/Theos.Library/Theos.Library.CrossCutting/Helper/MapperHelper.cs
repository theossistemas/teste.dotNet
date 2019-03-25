using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Theos.Library.CrossCutting.Helper
{
    public static class MapperHelper
    {
        public static IEnumerable<TDestiny> CopyList<TSource, TDestiny>(IEnumerable<TSource> src)
        {
            var ret = new List<TDestiny>();
            if (src == null)
            {
                return ret;
            }

            ret.AddRange(src.Select(origin => (TDestiny)Mapper.Map(origin, typeof(TSource), typeof(TDestiny))));
            return ret;
        }

        public static TDestiny Map<TSource, TDestiny>(TSource origin)
        {
            return Mapper.Map<TDestiny>(origin);
        }
    }
}
