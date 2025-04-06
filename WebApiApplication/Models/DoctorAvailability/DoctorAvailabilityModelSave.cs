namespace NetMed.WebApi.Models.DoctorAvailability
{
    public class DoctorAvailabilityModelSave
    {
        public int doctorID { get; set; }
        public DateOnly availableDate { get; set; }
        public TimeOnly startTime { get; set; }
        public TimeOnly endTime { get; set; }
    }
}
