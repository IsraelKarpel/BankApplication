namespace BankServer.Infrastructure.LoggerService
{
    /// <summary>
    /// Service that handlw the logger message
    /// </summary>
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception? ex = null);
    }
}
