
namespace NetMed.Application.Dtos.InsuranceProvider
{
    public class RemoveNetworkTypeDto : DtoBase
    {
        public int NetworkTypeId { get; set; }
        public bool Removed { get; set; }
    }
}
