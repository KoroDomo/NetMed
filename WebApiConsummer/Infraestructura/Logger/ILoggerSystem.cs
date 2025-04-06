namespace WebApiApplication.Infraestructura.Logger
{
    public interface ILoggerSystem
    {
        void LogInformation(string message);
        void LogWarning(string message);        
        void LogError(Exception ex, string message);
    }

}
