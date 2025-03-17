

namespace NetMed.Infraestructure.Messages
{
    public interface IMessageService
    {
        string GetMessage(string category, string key);
    }
}
