using NetMedWebApi.Infrastructure.Loggin.Interfaces;

namespace NetMedWebApi.Infrastructure.Loggin.Base
{
    public class LoggerCustom : ILoggerCustom
    {

        private readonly ILogger<LoggerCustom> _logger;


        public LoggerCustom(ILogger<LoggerCustom> logger)
        {
            _logger = logger;
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message);
        }

        public void LogError(Exception ex, string message, params object[] args)
        {
            _logger.LogError(ex, message);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message);
        }
    }
}
