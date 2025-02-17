using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NetMed.Domain.Entities
{
    public sealed class InsuranceProvider : PersonEntity
    {
        [Column("InsuranceProviderID")]
        [Key]
        public override int Id { get; set; }
        [Column("ContactNumber")] public new string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string? Website { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public string CoverageDetails { get; set; }
        public string? LogoUrl { get; set; }
        public bool IsPreferred { get; set; }
        public int NetworkTypeID { get; set; }
        public string? CustomerSupportContact { get; set; }
        
        public List<string>? AcceptedRegions { get; set; }
        public decimal? MaxCoverageAmount { get; set; }
    }
}
