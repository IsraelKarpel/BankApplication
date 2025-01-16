using System;

namespace BankServer.Infrastructure.LoggerService
{
    /// <summary>
    /// Service that handlw the logger message
    /// </summary>
    public class LoggerService : ILoggerService
    {
        private const string INFO = "INFO: ";
        private const string WARNING = "WARNING: ";
        private const string ERROR = "ERROR: ";
        private const string EXCEPTION = "Exception: ";
        public void LogInfo(string message)
        {
            Console.WriteLine(INFO + message);
        }

        public void LogWarning(string message)
        {
            Console.WriteLine(WARNING + message);
        }

        public void LogError(string message, Exception? ex = null)
        {
            Console.WriteLine(ERROR + message);
            if (ex != null)
            {
                Console.WriteLine(EXCEPTION + ex.Message);
            }
        }
    }
}
