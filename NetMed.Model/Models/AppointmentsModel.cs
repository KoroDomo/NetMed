
namespace NetMed.Model.Models
{
    public class AppointmentsModel
    {
        public int AppointmentID { get; set; } 
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; } = DateTime.Now;
        public int StatusID { get; set; }

    }
}
