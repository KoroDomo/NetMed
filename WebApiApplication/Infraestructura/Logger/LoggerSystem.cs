namespace WebApiApplication.Infraestructura.Logger
{
    public class LoggerSystem : ILoggerSystem
    {
        private readonly ILogger<LoggerSystem> _logger;

        public LoggerSystem(ILogger<LoggerSystem> logger) 
        {
            _logger = logger;
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
