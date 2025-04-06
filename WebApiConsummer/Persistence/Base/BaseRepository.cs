using NetMed.WebApi.Models.OperationsResult;
using System.Text;
using System.Text.Json;
using WebApiApplication.Models.OperationsResult;
using WebApiApplication.UrlConfi;

namespace WebApiApplication.Persistence.Base
{
    public class BaseRepository<TModel, TSave, TUpdate, TRemove> : IBaseRepository<TModel, TSave, TUpdate, TRemove>
    {
        protected readonly HttpClient _httpClient;

        public BaseRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(ApiConfig.GetBaseUrl());
        }

        public async Task<List<TModel>> GetAllAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode) return new List<TModel>();
            var result = await response.Content.ReadFromJsonAsync<OperationResultTypeList<TModel>>();
            return result?.data ?? new List<TModel>();
        }

        public async Task<TModel> GetByIdAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode) return default;
            var result = await response.Content.ReadFromJsonAsync<OperationResultType<TModel>>();
            return result.data;
        }

        public async Task<bool> SaveAsync(string endpoint, TSave model)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string endpoint, TUpdate model)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(string endpoint, TRemove model)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, endpoint)
            {
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
