
namespace NetMed.Application.Dtos.InsuranceProvider
{
    public class RemoveNetworkTypeDto : NetworkTypeDto
    {
        public int NetworkTypeId { get; set; }
        public bool Removed { get; set; }
    }
}
