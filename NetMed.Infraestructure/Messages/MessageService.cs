using NetMed.Infraestructure.Messages;
using Newtonsoft.Json;
using System.IO;
public class MessageService : IMessageService
{
    private readonly List<MessageItem> _messages;

    public MessageService(string jsonFilePath)
    {
        if (!File.Exists(jsonFilePath))
        {
            throw new FileNotFoundException($"El archivo JSON de mensajes no se encontró en: {jsonFilePath}");
        }

        var jsonContent = File.ReadAllText(jsonFilePath);
        var messagesWrapper = JsonConvert.DeserializeObject<MessagesWrapper>(jsonContent);

        _messages = messagesWrapper?.MessagesAppointments ?? new List<MessageItem>();
    }

    public string GetMessage(string methodName, bool isSuccess)
    {
        var message = _messages
            .Find(m => m.Method == methodName && m.Success == isSuccess);

        return message?.Message ?? "Mensaje no encontrado.";
    }
}