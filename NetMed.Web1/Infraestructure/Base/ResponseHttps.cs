using NetMed.ApiConsummer.Core.Repository;
using System.Text.Json;

namespace NetMed.ApiConsummer.Infraestructure.Base
{
    public class ResponseHttps : IResponseHttps
    {
        private readonly JsonSerializerOptions _jsonOptions;
        public ResponseHttps() 
        {
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<TEntity> ProcessResponse<TEntity>(HttpResponseMessage response)
        {
            try
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return await response.Content.ReadFromJsonAsync<TEntity>(_jsonOptions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
