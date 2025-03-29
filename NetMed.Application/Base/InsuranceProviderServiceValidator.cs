using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validators.Interfaces;
using NetMed.Persistence.Interfaces;

namespace NetMed.Infraestructure.Validators.Implementations
{
    public class InsuranceProviderServiceValidator : InsuranceProviderValidator, IInsuranceProviderServiceValidator
    {
        private readonly INetworkTypeRepository _networkTypeRepository;

		public InsuranceProviderServiceValidator(INetworkTypeRepository networkTypeRepository)
		{
            _networkTypeRepository = networkTypeRepository;

        }

        public async Task<OperationResult> ValidateNetworkTypeID(InsuranceProviders insuranceProviders)
        {
			var result = await _networkTypeRepository.GetNetworkTypeById(insuranceProviders.NetworkTypeID);
			if (result.Success == false)
			{
				result.Message = "El NetworkTypeID no es valido";
				return result;

			}
			return result;

        }
    }
}
