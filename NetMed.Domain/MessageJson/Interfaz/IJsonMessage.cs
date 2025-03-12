

namespace NetMed.Domain.MessageJson.Interfaz
{
   public interface  IJsonMessage
    {
        public Dictionary<string, string> ErrorMessages { get; }

        public Dictionary<string, string> SuccessMessages { get; }

    }
}
