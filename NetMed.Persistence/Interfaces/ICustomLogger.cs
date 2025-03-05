
namespace NetMed.Persistence.Interfaces
{
    public interface ICustomLogger
    {
        void LogInformation(string message);
        void LogError(Exception ex, string message);
        void LogWarning(string message);
    }
}
