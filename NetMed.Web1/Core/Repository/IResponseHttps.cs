namespace NetMed.ApiConsummer.Core.Repository
{
    public interface IResponseHttps
    {
        Task<TEntity> ProcessResponse<TEntity>(HttpResponseMessage response);
    }
}
