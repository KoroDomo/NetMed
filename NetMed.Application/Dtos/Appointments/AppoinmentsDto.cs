
namespace NetMed.Application.Dtos.Appointments
{
    public class AppoinmentsDto
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }

    }
}
