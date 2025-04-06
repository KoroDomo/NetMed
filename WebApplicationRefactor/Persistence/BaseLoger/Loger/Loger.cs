using NetMed.WebApplicationRefactor.Persistence.BaseLoger.Interface;
using Microsoft.Extensions.Logging;
using System;


namespace NetMed.WebApplicationRefactor.Persistence.BaseLoger.Loger
{
  
   


    public class Loger : ILoggerCustom
    {
        private readonly ILogger<Loger> _logger;

        public Loger(ILogger<Loger> logger)
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
            _logger.LogWarning(message);
        }
        

    }

}


