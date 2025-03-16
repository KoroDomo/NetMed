using Microsoft.Extensions.Logging;

namespace NetMed.Infraestructure.Logger
{
    public class CustomLogger: ICustomLogger
    {
        private readonly ILogger<CustomLogger> _logger;

        public CustomLogger(ILogger<CustomLogger> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogError(Exception ex, string message)
        {
            _logger.LogError(ex, message);
        }
        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }
    }
}
