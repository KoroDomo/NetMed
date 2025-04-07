namespace NetMedWebApi.Infrastructure.ApiClient.Interfaces
{
    public interface IResponseHttp
    {
        Task<T> ProcessResponse<T>(HttpResponseMessage reponse);
    }
}
