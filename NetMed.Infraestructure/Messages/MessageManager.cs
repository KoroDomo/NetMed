
using NetMed.Infraestructure.Messages;
using System.Text.Json;

public class MessageService : IMessageService
{
    private readonly Dictionary<string, Dictionary<string, string>> _messages;

    public MessageService()
    {
        var json = File.ReadAllText("JsonMessage.json");
        _messages = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);
    }

    public string GetMessage(string category, string key)
    {
        return _messages.ContainsKey(category) && _messages[category].ContainsKey(key)
            ? _messages[category][key]
            : "Mensaje no encontrado";
    }
}
