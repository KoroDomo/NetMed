

namespace NetMed.Persistence.BaseLoger.Interface
{
   public interface ILoggerCustom
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(Exception ex, string message);

    }
}
