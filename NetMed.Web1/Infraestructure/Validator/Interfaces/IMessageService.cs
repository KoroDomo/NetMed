namespace NetMed.ApiConsummer.Infraestructure.Validator.Interfaces
{
    public interface IMessageService
    {
        string GetSuccessMessage(string operationType);
        string GetErrorMessage(string operationType);
        public string GetServiceErrorMessage(string operationType);
    }
}
