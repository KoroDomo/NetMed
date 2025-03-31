
namespace NetMed.Domain.Base
{
    public class OperationResult
    {
        public OperationResult() 
        { 
          this.success = true;
        }    
        public string? message { get; set; }
        public bool success {get; set;}
        public dynamic? data { get; set; }
    }
}
