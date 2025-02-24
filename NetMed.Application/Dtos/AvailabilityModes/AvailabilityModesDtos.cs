namespace NetMed.Application.Dtos.AvailabilityModes
{
    public class AvailabilityModesDtos : DtoBase
    {
        public required string AvailabilityMode { get; set; }
        public required bool IsActive { get; set; }
    }
}
