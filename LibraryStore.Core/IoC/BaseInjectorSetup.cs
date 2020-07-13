using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryStore.Core.IoC
{
    internal class InjectorSetUpBase
    {
        public static IEnumerable<U> LoadAssemblies<T, U>()
        {
            return typeof(T).Assembly
                        .ExportedTypes
                        .Where(x => typeof(U).IsAssignableFrom(x)
                                 && !x.IsInterface
                                 && !x.IsAbstract)
                        .Select(Activator.CreateInstance)
                        .Cast<U>();
        }
    }
}