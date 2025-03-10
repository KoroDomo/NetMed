
namespace NetMed.Domain.Base
{
    public class OperationResult
    {
        public OperationResult() 
        { 
            Success = false;
        }
        public string Message { get; set; }
        public bool Success {get; set;}
        public dynamic Result { get; set; }
    }
}
