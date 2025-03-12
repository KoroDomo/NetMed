

using Microsoft.Extensions.Logging;
using NetMed.Infrastructure.Loggin.NetMed.Application.Interfaces;

namespace NetMed.Infraestructure.Loggin.Base
{
    public class LoggerCustom : ILoggerCustom
    {
        private readonly ILogger<LoggerCustom> _logger;

        public LoggerCustom(ILogger<LoggerCustom> logger)
        {
            _logger = logger;
        }

        public void LogError(string message)
        {
           _logger.LogError(message);
        }

        public void LogError(Exception ex, string message)
        {
            _logger.LogError(ex, message);
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }
    }
}
