
namespace NetMed.Application.Dtos.Appointments
{
    public class AppointmentsDto : BaseDto
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }

    }
}
