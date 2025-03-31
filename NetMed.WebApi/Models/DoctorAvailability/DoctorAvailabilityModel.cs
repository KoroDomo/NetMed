namespace NetMed.WebApi.Models.DoctorAvailability
{
    public class DoctorAvailabilityModel
    {
        public int availabilityID { get; set; }
        public int doctorID { get; set; }
        public DateOnly availableDate { get; set; }
        public TimeOnly startTime { get; set; }
        public TimeOnly endTime { get; set; }
    }
}
