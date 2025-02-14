

namespace NetMed.Model.Models
{
    public class DoctorAvailabilityModel
    {
        public int AvailabilityID { get; set; }
        public int DoctorID { get; set; }
        public DateOnly AvailableDate { get; set; }
        public TimeOnly StarTime { get; set; }
        public TimeOnly EndTime { get;  set; }
    }
}
