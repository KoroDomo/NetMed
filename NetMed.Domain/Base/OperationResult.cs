
namespace NetMed.Domain.Base
{
    public class OperationResult
    {
        public OperationResult() 
        { 
          this.success = true;
        }    
        public string Message { get; set; }
        public bool success {get; set;}
        public dynamic Data { get; set; }
    }
}
