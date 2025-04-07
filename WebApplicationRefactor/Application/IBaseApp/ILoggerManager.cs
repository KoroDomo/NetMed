namespace WebApplicationRefactor.Application.IBaseApp
{
    public interface ILoggerManager
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(Exception ex, string message);

    }
}
