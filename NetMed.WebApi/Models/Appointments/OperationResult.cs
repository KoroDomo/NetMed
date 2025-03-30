namespace NetMed.WebApi.Models.Appointments
{
    public class OperationResult
    {
        public string Message { get; set; }
        public bool success { get; set; }
        public List<AppointmentsModel> Data { get; set; }
    }
}
