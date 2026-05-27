namespace WebApplication4.TrafficLight;
using WebApplication4.TrafficLight.models;
using System.Text.Json;

public class CreateJson1
{
    public static string trainJsonClossingCate(string TrainName)
    {
        var data = new RootSendMessages
        {
            timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            messages = []
        };

       
        
            data.messages.Add(new sendMessages()
            {
                type = "crossingGate",
                message = new sendMessageContent
                {
                    id = TrainName,
                    state = "closed",
                }
            });
        
        
        var option = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(data, option);
        return json;
        
    }

    public static string trainJsonopeningCate(string TrainName)
    {
        var data = new RootSendMessages
        {
            timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            messages = []
        };

        foreach (var item in TrainName)
        {
            data.messages.Add(new sendMessages()
            {
                type = "crossingGate",
                message = new sendMessageContent
                {
                    id = item.ToString(),
                    state = "opening",
                }
            });
        }
        
        var option = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(data, option);
        return json;
    }
}