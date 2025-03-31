using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NetMed.Domain.Entities
{
    [Table("InsuranceProviders", Schema = "Insurance")]
    public sealed class InsuranceProviders : PersonEntity
    {
        [Column("InsuranceProviderID")]
        [Key]
        public override int Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
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
        public string? AcceptedRegions { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? MaxCoverageAmount { get; set; }

        
    }
}
