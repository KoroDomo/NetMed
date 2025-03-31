

using NetMed.WebApi.Models.Appointments.Base;

namespace NetMed.WebApi.Models.Appointments
{
    public class AppointmentsModel : BaseAppointmentsModel
    {
        public int appointmentID { get; set; }
        public int patientID { get; set; }
        public int doctorID { get; set; }
        public DateTime appointmentDate { get; set; }
        public int statusID { get; set; }
    }
}
