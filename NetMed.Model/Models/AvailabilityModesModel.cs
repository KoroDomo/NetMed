

namespace NetMed.Model.Models
{
    public class AvailabilityModesModel
    {
        public required int SAvailabilityModeID { get; set; }
        public required string AvailabilityMode { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public required bool IsActive { get; set; }
    }
}
