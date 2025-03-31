namespace NetMed.Web1.Models
{
    public class ListOperationResult<TEntity>
    {

        public string Message { get; set; }
        public bool success { get; set; }
        public List<TEntity> Result { get; set; }

    }

    public class OperationResult<TEntity>
    {

        public string Message { get; set; }
        public bool success { get; set; }
        public TEntity Result { get; set; }

    }
}
