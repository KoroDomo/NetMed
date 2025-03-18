

namespace NetMed.Infraestructure.Messages
{
    public interface IMessageService
    {
        string GetMessage(string methodName, bool isSuccess);
    }
}
