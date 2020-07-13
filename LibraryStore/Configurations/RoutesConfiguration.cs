namespace LibraryStore.Configurations
{
    public static class RoutesConfiguration
    {
        const string URL_ROOT = "api";
        const string VERSION = "v1";

        const string ROUTE_PREFIX = URL_ROOT + "/" + VERSION;

        public const string PARAM_GUID = "{id}";

        public static class Book
        {
            const string ROUTE_PREFIX = RoutesConfiguration.ROUTE_PREFIX + "/book";

            public const string GET_ALL = ROUTE_PREFIX + "s";
            public const string GET_BY_ID = ROUTE_PREFIX + "/" + PARAM_GUID;
            public const string POST = ROUTE_PREFIX;
            public const string PUT = ROUTE_PREFIX + "/" + PARAM_GUID;
            public const string DELETE = ROUTE_PREFIX + "/" + PARAM_GUID;
        }

        public static class User
        {
            const string ROUTE_PREFIX = RoutesConfiguration.ROUTE_PREFIX + "/user";

            public const string GET_ALL = ROUTE_PREFIX + "s";
            public const string GET_BY_ID = ROUTE_PREFIX + "/" + PARAM_GUID;
            public const string POST = ROUTE_PREFIX;
            public const string PUT = ROUTE_PREFIX + "/" + PARAM_GUID;
            public const string DELETE = ROUTE_PREFIX + "/" + PARAM_GUID;
        }
    }
}