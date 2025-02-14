
namespace NetMed.Model.Models
{
    public class AppointmentsModel
    {
        public int AppointmentID { get; set; } 
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public int StatusID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
    }
}
