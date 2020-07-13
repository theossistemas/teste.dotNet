using System.Collections.Generic;

namespace LibraryStore.Core.Mappers
{
    public interface IMapper<TParam, TResult>
        where TParam : class
        where TResult : class
    {
        TResult ToMap(TParam obj);
        TResult ToMap(TParam obj, TResult result);
        IList<TResult> ToMapList<T>(T list)
            where T : IList<TParam>, ICollection<TParam>;
    }
}