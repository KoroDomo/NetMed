using System.Text;
using System.Text.Json;

namespace NetMed.ApiConsummer.Persistence.Base
{
    public abstract class BaseRepository 
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        protected virtual string BaseEndpoint { get; } = "Api/";

        public BaseRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<TEntity> ProcessResponse<TEntity>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode == false)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"API Error: {response.StatusCode} - {errorContent}");
            }

            return await response.Content.ReadFromJsonAsync<TEntity>(_jsonOptions);
        }

        protected virtual async Task<TResult> GetAsync<TResult>(string endpoint, int id)
        {
            var response = await _httpClient.GetAsync($"{BaseEndpoint}{endpoint}{id}");
            return await ProcessResponse<TResult>(response);
        }

        protected virtual async Task<TResult> GetAllAsync<TResult>(string endpoint)
        {
            var response = await _httpClient.GetAsync($"{BaseEndpoint}{endpoint}");
            return await ProcessResponse<TResult>(response);
        }

        protected virtual async Task<TResult> CreateAsync<TResult>(string endpoint, TResult model)
        {
            var response = await _httpClient.PostAsJsonAsync(
                $"{BaseEndpoint}{endpoint}",
                model,
                _jsonOptions
            );
            return await ProcessResponse<TResult>(response);
        }

        protected virtual async Task<TResult> UpdateAsync<TResult>(string endpoint, int id, TResult model)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"{BaseEndpoint}{endpoint}/{id}",
                model,
                _jsonOptions
            );
            return await ProcessResponse<TResult>(response);
        }

        protected virtual async Task<TResult> DeleteAsync<TResult>(string endpoint, int id, TResult model)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{BaseEndpoint}{endpoint}")
            {
                Content = new StringContent(JsonSerializer.Serialize(model, _jsonOptions),
                Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            return await ProcessResponse<TResult>(response);
        }
    }
}

