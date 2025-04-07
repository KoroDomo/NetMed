namespace WebApplicationRefactor.Services.Interface
{
    public interface IErrorMessageService
    {
        string GetErrorMessage(string category, string key);
    }
}