using NetMed.ApiConsummer.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMed.ApiConsummer.Core.Models.InsuranceProvider
{
    public class InsuranceProviderModel : BaseModel
    {
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? Website { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public string CoverageDetails { get; set; }
        public bool IsPreferred { get; set; }
        public int NetworkTypeID { get; set; }
        public string? CustomerSupportContact { get; set; }
        public string? AcceptedRegions { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? MaxCoverageAmount { get; set; }
    }
}
