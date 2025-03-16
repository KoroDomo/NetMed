
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;

namespace NetMed.Infraestructure.Validators.Interfaces
{
    public interface INetworkTypeValidator : IOperationValidator
    {
        public OperationResult ValidateNetworkType(NetworkType networkType);
        public OperationResult ValidateNameExists(NetworkType networkType, NetMedContext context);

    }
}
