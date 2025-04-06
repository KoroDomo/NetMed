namespace WebApplicationRefactor.Persistence.Config
{
    public abstract class BaseApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
    
    protected BaseApi(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["UrlSet:BaseUrl"]);
        }

        protected async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();    
                return await response.Content.ReadFromJsonAsync<T>();
        }
        protected async Task<T> PostAsync<T>(string url, T data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        protected async Task<T> PutAsync<T>(string url, T data)
        {
            var response = await _httpClient.PutAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        protected async Task DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}
