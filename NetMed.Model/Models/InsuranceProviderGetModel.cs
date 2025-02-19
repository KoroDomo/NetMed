

namespace NetMed.Model.Models
{
    internal class InsuranceProviderGetModel
    {
        public int InsuranceProviderID { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public bool IsPrefered { get; set; }
        public int NetworkTypeID { get; set; }
        public int? CustomerSupportContact { get; set; }
        public decimal? MaxCoverageAmount { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
