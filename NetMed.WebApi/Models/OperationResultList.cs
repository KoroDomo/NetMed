namespace NetMed.Web.Models
{
    public class OperationResultList<T> 
    {
    
        public string Message { get; set; }
        public bool Success { get; set; }
        
        public List<T> data { get; set; }
    }
}
