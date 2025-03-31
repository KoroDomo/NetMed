
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMed.Application.Dtos.Appointments
{
    public class SaveAppointmentsDto 
    {
        public int patientID { get; set; }
        public int doctorID { get; set; }
        public DateTime appointmentDate { get; set; }
        public int statusID { get; set; }
    }
}
