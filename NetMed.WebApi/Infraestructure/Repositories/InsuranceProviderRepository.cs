using NetMed.ApiConsummer.Core.Base;
using NetMed.ApiConsummer.Core.Models.InsuranceProvider;
using NetMed.ApiConsummer.Persistence.Base;
using NetMed.ApiConsummer.Persistence.Interfaces;

namespace NetMed.ApiConsummer.Persistence.Repositories
{
    public class InsuranceProviderRepository : BaseRepository, IInsuranceProviderRepository
    {
        protected override string BaseEndpoint => "InsuranceProviderApi/";
        public InsuranceProviderRepository(HttpClient httpClient) : base(httpClient)
        { }        
        public async Task<OperationResult> CreateProviderAsync(SaveInsuranceProviderModel insuranceProvider)
        {
            var result = new OperationResult();
            try
            {
                result.Result = await CreateAsync("SaveInsuranceProvider", insuranceProvider);
                return result;
            }
            catch (Exception ex) 
            {
                result.Message = ex.Message;
                return result;
            }
            
            //var response = await _httpClient.PostAsJsonAsync($"{BaseEndpoint}SaveInsuranceProvider", insuranceProvider);
            //await ProcessResponse<object>(response);
        }
        public async Task<OperationResult> DeleteProviderAsync(RemoveInsuranceProviderModel insurance)
        {
            var result = new OperationResult();
            try
            {
                result.Result = await DeleteAsync("RemoveInsuranceProvider", insurance.InsuranceProviderID, insurance);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
            //var request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Delete,
            //    RequestUri = new Uri(_httpClient.BaseAddress, $"{BaseEndpoint}RemoveInsuranceProvider"),
            //    Content = new StringContent(JsonSerializer.Serialize(insurance), Encoding.UTF8, "application/json")
            //};

            //var response = await _httpClient.SendAsync(request);
            //await ProcessResponse<object>(response);
        }

        public async Task<OperationResult> GetProviderByIdAsync<TEntity>(int id)
        {
            var result = new OperationResult();
            try
            {
                result.Result = await GetAsync<TEntity>("GetInsuranceProviderBy", id);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
            //var response = await _httpClient.GetAsync($"{BaseEndpoint}GetInsuranceProviderBy{id}");
            //return await ProcessResponse<OperationResult<TEntity>>(response);
        }

        public async Task<OperationResult> GetAllProvidersAsync()
        {
            var result = new OperationResult();
            try 
            {
                result.Result = await GetAllAsync<List<GetInsuranceProviderModel>>("GetInsuranceProviders");
                return result;
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }

        //    var response = await _httpClient.GetAsync($"{BaseEndpoint}GetInsuranceProviders");
        //    return await ProcessResponse<ListOperationResult<GetInsuranceProviderModel>>(response);
        }

        public async Task<OperationResult> UpdateProviderAsync(UpdateInsuranceProviderModel insuranceProvider)
        {
            var result = new OperationResult();
            try
            {
                result.Result = await UpdateAsync("UpdateInsuranceProvider", insuranceProvider.InsuranceProviderID, insuranceProvider);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
            //var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}UpdateInsuranceProvider", insuranceProvider);
            //await ProcessResponse<object>(response);
        }
    }
}
