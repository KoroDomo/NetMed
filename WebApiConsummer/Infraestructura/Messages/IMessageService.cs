namespace WebApiApplication.Infraestructura.Messages
{
    public interface IMessageService
    {
        string GetMessage(string methodName, bool isSuccess);
    }
}
