using NetMedWebApi.Infrastructure.ApiClient.Interfaces;
using System.Text.Json;

namespace NetMedWebApi.Infrastructure.ApiClient.Base
{
    public class ResponseHttp : IResponseHttp
    {
        private readonly JsonSerializerOptions _jsonOptions;
        public ResponseHttp()
        {

            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<T> ProcessResponse<T>(HttpResponseMessage reponse)
        {

            string apiResponse = await reponse.Content.ReadAsStringAsync();

            try
            {
                var result = await reponse.Content.ReadAsStringAsync();
                return await reponse.Content.ReadFromJsonAsync<T>(_jsonOptions);

            }
            catch (Exception ex)
            {

                throw ex;
            
            }
        }
    }
}
