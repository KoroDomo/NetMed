
namespace NetMed.Application.Dtos.InsuranceProvider
{
    public class RemoveInsuranceProviderDto : DtoBase
    {
        public int InsuranceProviderID { get; set; }
        public bool Removed{ get; set; }
    }
}
