
namespace NetMed.Domain.Base
{
    public class OperationResult
    {
        public OperationResult()
        {
            this.Message = string.Empty; 
            this.Success = true;
            this.Result = null;
        }
        public string Message { get; set; }
        public bool Success {get; set;}
        public dynamic? Result { get; set; }

    }
}
