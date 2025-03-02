
namespace NetMed.Application.Dtos.DoctorAvailability
{
    public class DoctorAvailabilityDto
    {
        public int DoctorID { get; set; }
        public DateOnly AvailableDate { get; set; }
        public TimeOnly StarTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
