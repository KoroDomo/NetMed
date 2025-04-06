namespace NetMed.WebApi.Models.Appointments
{
    public class AppointmentsModelSave
    {
        public int patientID { get; set; }
        public int doctorID { get; set; }
        public DateTime appointmentDate { get; set; }
        public int statusID { get; set; }
    }
}
