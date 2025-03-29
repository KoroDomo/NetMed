
namespace NetMed.Application.Dtos.InsuranceProvider
{
    public class RemoveInsuranceProviderDto : InsuranceProviderDto
    {
        public int InsuranceProviderID { get; set; }
        public bool Removed{ get; set; }
    }
}
