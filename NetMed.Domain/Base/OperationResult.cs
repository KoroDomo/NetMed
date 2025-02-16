
namespace NetMed.Domain.Base
{
    public class OperationResult
    {
        public OperationResult()
        {
            this.Message = string.Empty; 
            this.Success = true;
            this.data = null;

        }
        public string Message { get; set; }
        public bool Success {get; set;}
        public dynamic? data { get; set; }

    }
}
