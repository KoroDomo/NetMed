using NetMedWebApi.Infrastructure.ApiClient.Interfaces;
using NetMedWebApi.Infrastructure.ApiConfig;
using NetMedWebApi.Models;
using System.Text.Json;

namespace NetMedWebApi.Infrastructure.ApiClient.Base
{
    public class ClientApi : ResponseHttp, IClientApi
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public string BaseEndPoint { get; set; } = "api/";

        public ClientApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _httpClient.BaseAddress = new Uri(UrlBase.GetBaseUrl());
        }

      

        public virtual async Task<OperationResultList<T>> GetAllAsync<T>(string uri)
        {

            try
            {
                var reponse = await _httpClient.GetAsync($"{BaseEndPoint}{uri}");
                return await ProcessResponse<OperationResultList<T>>(reponse);
            }

            catch (Exception ex)
            {
                return new OperationResultList<T> { Success = false, Message = ex.Message };

            }
        }

        public async virtual Task<OperationResult<T>> PutAsync<T>(string uri, T model)
        {
            try
            {
                var reponse = await _httpClient.PutAsJsonAsync($"{BaseEndPoint}{uri}", _jsonOptions); ;

                return await ProcessResponse<OperationResult<T>>(reponse);
            }
            catch (Exception ex)
            {
                return new OperationResult<T> { Success = false, Message = ex.Message };

            }

        }

        public async virtual Task<OperationResult<T>> GetByIdAsync<T>(string uri, int id)
        {
            try
            {
                var reponse = await _httpClient.GetAsync($"{BaseEndPoint}{uri}{id}");
                return await ProcessResponse<OperationResult<T>>(reponse);
            }

            catch (Exception ex)
            {
                return new OperationResult<T> { Success = false, Message = ex.Message };

            }
        }

        public virtual async Task<OperationResult<T>> PostAsync<T>(string uri, T model)
        {
            try
            {
                var reponse = await _httpClient.PostAsJsonAsync($"{BaseEndPoint}{uri}", _jsonOptions);

                return await ProcessResponse<OperationResult<T>>(reponse);
            }
            catch (Exception ex)
            {
                return new OperationResult<T> { Success = false, Message = ex.Message };

            }
        }

        public virtual async Task<OperationResult<T>> DeleteAsync<T>(string uri, T model)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, uri)
                {
                    Content = new StringContent(JsonSerializer.Serialize(model, _jsonOptions), Encoding.UTF8, "application/json")
                };

                var reponse = await _httpClient.SendAsync(request);
                return await ProcessResponse<OperationResult<T>>(reponse);

                ;


            }
            catch (Exception ex)
            {
                return new OperationResult<T> { Success = false, Message = ex.Message };

            }

        }

    }
}
    
