using NetMed.ApiConsummer.Core.Models.NetworkType;
using NetMed.ApiConsummer.Persistence.Base;
using NetMed.ApiConsummer.Persistence.Interfaces;

namespace NetMed.ApiConsummer.Persistence.Repositories
{
    public class NetworkTypeRepository : BaseRepository, INetworkTypeRepository
    {
        public NetworkTypeRepository(HttpClient httpClient) : base(httpClient)
        {
            
        }

        public async Task CreateNetworkAsync(SaveNetworkTypeModel networkType)
        {
            networkType.ChangeDate = DateTime.Now;
            await CreateAsync("SaveNetworkTypeRepository", networkType);
            //networkType.ChangeDate = DateTime.Now;
            //var response = await _httpClient.PostAsJsonAsync("NetworkTypeApi/SaveNetworkTypeRepository", networkType);
            //await ProcessResponse<object>(response);
        }

        public async Task DeleteNetworkAsync(RemoveNetworkTypeModel networkType)
        {
            await DeleteAsync("RemoveInsuranceProvider", networkType.NetworkTypeId, networkType);
            //var request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Delete,
            //    RequestUri = new Uri(_httpClient.BaseAddress, "NetworkTypeApi/RemoveInsuranceProvider"),
            //    Content = new StringContent(JsonSerializer.Serialize(networkType), Encoding.UTF8, "application/json")
            //};

            //var response = await _httpClient.SendAsync(request);
            //await ProcessResponse<object>(response);
        }

        public async Task<TEntity>GetNetworkByIdAsync<TEntity>(int id)
        {
            return await GetAsync<TEntity>("GetInsuranceProviderBy", id);
            //var response = await _httpClient.GetAsync($"NetworkTypeApi/GetNetworkTypeRepositoryBy{id}");
            //return await ProcessResponse<OperationResult<TEntity>>(response);
        }

        public async Task<List<GetNetworkTypeModel>>GetAllNetworksAsync()
        {
            return await GetAllAsync<List<GetNetworkTypeModel>>("GetNetworkTypeRepositorys");
            //var response = await _httpClient.GetAsync("NetworkTypeApi/GetNetworkTypeRepositorys");
            //return await ProcessResponse<ListOperationResult<GetNetworkTypeModel>>(response);
        }

        public async Task UpdateNetworkAsync(UpdateNetworkTypeModel networkType)
        {

            networkType.ChangeDate = DateTime.Now;
            await UpdateAsync("UpdateNetworkTypeRepository", networkType.NetworkTypeId, networkType);
            //networkType.ChangeDate = DateTime.Now;
            //var response = await _httpClient.PutAsJsonAsync("NetworkTypeApi/UpdateNetworkTypeRepository", networkType);
            //await ProcessResponse<object>(response);
        }
    }
}
