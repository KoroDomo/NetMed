using NetMedWebApi.Application.Base;
using NetMedWebApi.Models.Status;

namespace NetMedWebApi.Application.Contracts
{
    public interface IStatusContract : IBaseContract<StatusApiModel, SaveStatusModel, UpdateStatusModel, DeleteStatusModel>
    {
    
    }
}
