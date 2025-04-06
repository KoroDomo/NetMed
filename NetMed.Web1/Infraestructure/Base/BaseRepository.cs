using NetMed.ApiConsummer.Core.Base;
using NetMed.ApiConsummer.Core.Repository;
using System.Text;
using System.Text.Json;

namespace NetMed.ApiConsummer.Infraestructure.Base
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public string BaseEndpoint { get; set; } = "Api/";

        public BaseRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
            _httpClient.BaseAddress = new Uri(ApiConfig.GetBaseUrl());
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

        public virtual async Task<OperationResult<TResult>> GetAsync<TResult>(string endpoint, int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseEndpoint}/{endpoint}{id}");
                return await ProcessResponse<OperationResult<TResult >> (response);

            }
            catch (Exception ex)
            {
                return new OperationResult<TResult> { Message = ex.Message };
            }
        }

        public virtual async Task<ListOperationResult<TResult>> GetAllAsync<TResult>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseEndpoint}/{endpoint}");
                return await ProcessResponse<ListOperationResult<TResult>>(response);

            }
            catch (Exception ex)
            {
                return new ListOperationResult<TResult> { Message=ex.Message };
            }
        }

        public virtual async Task<OperationResult<TResult>> CreateAsync<TResult>(string endpoint, TResult model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    $"{BaseEndpoint}/{endpoint}",
                    model,
                    _jsonOptions
                );
                return await ProcessResponse<OperationResult<TResult>>(response);

            }
            catch (Exception ex)
            {

                return new OperationResult<TResult> { Message = ex.Message };
            }
        }

        public virtual async Task<OperationResult<TResult>> UpdateAsync<TResult>(string endpoint, TResult model)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(
                    $"{BaseEndpoint}/{endpoint}",
                    model,
                    _jsonOptions
                );
                return await ProcessResponse<OperationResult<TResult>>(response);

            }
            catch (Exception ex)
            {

                return new OperationResult<TResult> { Message = ex.Message };
            }
        }

        public virtual async Task<OperationResult<TResult>> DeleteAsync<TResult>(string endpoint, TResult model)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"{BaseEndpoint}/{endpoint}")
                {
                    Content = new StringContent(JsonSerializer.Serialize(model, _jsonOptions),
                    Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);
                return await ProcessResponse<OperationResult<TResult>>(response);

            }
            catch (Exception ex)
            {

                return new OperationResult<TResult> { Message = ex.Message };
            }
        }
    }
}