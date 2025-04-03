using NetMedWebApi.Models.Notification;

namespace NetMedWebApi.Models
{
    public class OperationResultList <T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public List<T> Data { get; set; }

    }
    public class OperationResultSingle <T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public T Data { get; set; }

    }

}
