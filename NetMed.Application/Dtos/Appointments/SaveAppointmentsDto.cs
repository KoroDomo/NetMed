
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMed.Application.Dtos.Appointments
{
    public class SaveAppointmentsDto 
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }
    }
}
