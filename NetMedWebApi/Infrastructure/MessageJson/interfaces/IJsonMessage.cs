namespace NetMedWebApi.Infrastructure.MessageJson.interfaces
{
    public interface IJsonMessage
    {
        public Dictionary<string, string> ErrorMessages { get; }

        public Dictionary<string, string> SuccessMessages { get; }

        public Dictionary<string, string> ErrorNotification { get; }

    }
}
