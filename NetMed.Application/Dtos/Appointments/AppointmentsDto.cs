namespace NetMed.Application.Dtos.Appointments
{
    public class AppointmentsDto : BaseDto
    {
        public int appointmentID { get; set; }
        public int patientID { get; set; }
        public int doctorID { get; set; }
        public DateTime appointmentDate { get; set; }
        public int statusID { get; set; }

    }
}
