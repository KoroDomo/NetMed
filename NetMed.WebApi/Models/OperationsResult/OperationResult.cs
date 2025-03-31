using NetMed.WebApi.Models.Appointments;

namespace NetMed.WebApi.Models.OperationsResult
{
    public class OperationResult<T>
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public T data { get; set; }
    }
}
