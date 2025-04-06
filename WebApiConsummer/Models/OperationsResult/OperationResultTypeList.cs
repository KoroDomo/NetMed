

namespace WebApiApplication.Models.OperationsResult
{
    public class OperationResultTypeList<T>
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public List<T> data { get; set; }
    }
}
