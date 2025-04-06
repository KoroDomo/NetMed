using NetMed.ApiConsummer.Application.Base;
using NetMed.ApiConsummer.Core.Models.NetworkType;

namespace NetMed.ApiConsummer.Application.Contracts
{
    public interface INetworkTypeService : IBaseService<GetNetworkTypeModel,SaveNetworkTypeModel, UpdateNetworkTypeModel, RemoveNetworkTypeModel>
    {


    }
}
