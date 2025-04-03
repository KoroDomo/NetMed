namespace NetMed.WebApi.Models
{
    public class OperationResult<T>
    {
  

        public T data { get; set; } 
        public bool Success { get; set; }

        
    }
}
