using System.Text.Json;
using WebApplication4.TrafficLight.models;


namespace WebApplication4.TrafficLight;

public class CreateJson
{
    public static string GenerateJsonStructure(double[] trafficeLightOff, double[] trafficeLightOn )
    {
        

        var data = new RootSendMessages
        {
            timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            messages = []
        };
        
        
        foreach (var item in trafficeLightOff)
        {
            data.messages.Add(new sendMessages()
            {
                type = "trafficLightState",
                message = new sendmessageContent
                {
                    id = item,
                    state = "red",
                }
            });
        }
        
        foreach (var item in trafficeLightOn)
        {
            data.messages.Add(new sendMessages()
            {
                type = "trafficLightState",
                message = new sendmessageContent
                {
                    id = item,
                    state = "green",
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