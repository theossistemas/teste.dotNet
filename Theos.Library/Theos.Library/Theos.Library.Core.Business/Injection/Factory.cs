using Microsoft.Extensions.DependencyInjection;

namespace Theos.Library.Core.Business.Injection
{
    public static class Factory
    {
        private static ServiceProvider _services;

        public static void SetServices(ServiceProvider services)
        {
            _services = services;
        }

        public static TS GetInstance<T, TS>()
        {
            return (TS)_services.GetService(typeof(T));
        }
    }
}
