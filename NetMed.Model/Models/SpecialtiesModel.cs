

namespace NetMed.Model.Models
{
    public class SpecialtiesModel
    {
        public required int SpecialtyID { get; set; }
        public required string SpecialtyName { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public required bool IsActive { get; set; }
    }
}
