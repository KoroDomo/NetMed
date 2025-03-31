
namespace NetMed.Application.Dtos.DoctorAvailability
{
    public class SaveDoctorAvailabilityDto 
    {
        public int doctorID { get; set; }
        public DateOnly availableDate { get; set; }
        public TimeOnly startTime { get; set; }
        public TimeOnly endTime { get; set; }

    }
}
