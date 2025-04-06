namespace NetMed.ApiConsummer.Core.Base
{
    public class ListOperationResult<TEntity>
    {
        public string Message { get; set; }
        public bool success { get; set; }
        public List<TEntity> Result { get; set; }
    }

    public class OperationResult<TEntity>
    {
        public OperationResult() 
        {
            Success = false;
            Message = string.Empty;
        }
        public string Message { get; set; }
        public bool Success { get; set; }
        public TEntity Result { get; set; }
    }
}
