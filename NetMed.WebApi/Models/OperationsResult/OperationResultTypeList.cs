using NetMed.WebApi.Models.Appointments;

namespace NetMed.WebApi.Models.OperationsResult
{
    public class OperationResultTypeList<T>
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public List<T> data { get; set; }
    }
}
