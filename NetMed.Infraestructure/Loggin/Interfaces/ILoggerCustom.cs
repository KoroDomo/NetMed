
namespace NetMed.Infrastructure.Loggin
{
    namespace NetMed.Application.Interfaces
    {
        public interface ILoggerCustom
        {
            void LogInformation(string message);
            void LogWarning(string message);
            void LogError(string message);
            void LogError(Exception ex, string message);
        }
    }
}
