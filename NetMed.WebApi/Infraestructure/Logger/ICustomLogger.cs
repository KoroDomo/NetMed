
namespace NetMed.ApiConsummer.Persistence.Logger
{
    public interface ICustomLogger
    {
        void LogInformation(string message);
        void LogError(Exception ex, string message);
        void LogWarning(string message);
    }
}
