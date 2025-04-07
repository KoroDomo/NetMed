using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebApplicationRefactor.Persisten.Configuration
{
    public abstract class BaseApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        protected BaseApi(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]);
        }

        protected async Task<T?> GetAsync<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        protected async Task PostAsync<T>(string uri, T entity)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, entity);
            response.EnsureSuccessStatusCode();
        }

        protected async Task PutAsync<T>(string uri, T entity)
        {
            var response = await _httpClient.PutAsJsonAsync(uri, entity);
            response.EnsureSuccessStatusCode();
        }

        protected async Task DeleteAsync(string uri)
        {
            var response = await _httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
        }
    }
}