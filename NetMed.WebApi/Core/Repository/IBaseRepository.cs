namespace NetMed.ApiConsummer.Core.Repository
{
    public interface IBaseRepository
    {
        Task<TEntity> ProcessResponse<TEntity>(HttpResponseMessage response);
        Task<TResult> GetAsync<TResult>(string endpoint, int id);
        Task<TResult> GetAllAsync<TResult>(string endpoint);
        Task<TResult> CreateAsync<TResult, TCreate>(string endpoint, TCreate model);
        Task<TResult> UpdateAsync<TResult, TUpdate>(string endpoint, int id, TUpdate model);
        Task<TResult> DeleteAsync<TResult, TDelete>(string endpoint, int id, TDelete model);

    }
}
