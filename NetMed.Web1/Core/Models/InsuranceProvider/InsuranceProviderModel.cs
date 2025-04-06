using NetMed.ApiConsummer.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMed.ApiConsummer.Core.Models.InsuranceProvider
{
    public class InsuranceProviderModel : BaseModel
    {
        public string name { get; set; }
        public string contactNumber { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string? website { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? country { get; set; }
        public string? zipCode { get; set; }
        public string coverageDetails { get; set; }
        public bool isPreferred { get; set; }
        public int networkTypeID { get; set; }
        public string? customerSupportContact { get; set; }
        public string? acceptedRegions { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? maxCoverageAmount { get; set; }
    }
}
