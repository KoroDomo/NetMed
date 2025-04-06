using NetMed.ApiConsummer.Core.Base;

namespace NetMed.ApiConsummer.Core.Models.NetworkType
{
    public class NetworkTypeModel : BaseModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
