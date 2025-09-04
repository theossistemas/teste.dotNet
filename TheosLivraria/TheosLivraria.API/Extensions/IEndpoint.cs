namespace TheosLivraria.API.Extensions
{
    public interface IEndpoint
    {
        static abstract void Map(IEndpointRouteBuilder app);
    }
}
