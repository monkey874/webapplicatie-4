using System.Text.Json;
using WebApplication4.TrafficLight.models;
using WebApplication4.TrafficLight.sort;

namespace WebApplication4.TrafficLight;

public class createJson
{
    public string generateJsonStructure(double[] trafficeLightOff, double[] trafficeLightOn )
    {
        
        var option = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        
        var data = new RootSendMessages
        {
            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            Messages = new List<sendMessages>()
        };
        
        
        foreach (var item in trafficeLightOff)
        {
            data.Messages.Add(new sendMessages()
            {
                Type = "trafficLightState",
                Message = new sendmessageContent
                {
                    Id = item,
                    State = "red",
                }
            });
        }
        
        foreach (var item in trafficeLightOn)
        {
            data.Messages.Add(new sendMessages()
            {
                Type = "trafficLightState",
                Message = new sendmessageContent
                {
                    Id = item,
                    State = "green",
                }
            });
        }

        string json = JsonSerializer.Serialize(data, option);
        return json;
    }
    

  
    
}