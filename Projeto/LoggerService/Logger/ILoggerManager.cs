namespace LoggerService.Logger
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogError(string message);
    }
}
