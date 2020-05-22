namespace TheosBookStore.LibCommon.Services
{
    public interface IServiceBase
    {
        string GetErrorMessages();

        bool IsValid { get; }
    }
}
