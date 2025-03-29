
namespace NetMed.Application.Dtos.NetworkType
{
    public class RemoveNetworkTypeDto : NetworkTypeDto
    {
        public int NetworkTypeId { get; set; }
        public bool Removed { get; set; }
    }
}
