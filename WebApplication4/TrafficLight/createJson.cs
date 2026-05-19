using System.Text.Json;
using System.Collections.Generic;
using WebApplication4.TrafficLight.models;
using System;
using WebApplication4.TrafficLight.sort;

namespace WebApplication4.TrafficLight;

public class createJson
{
    public string generateJsonStructure(double[] trafficeLightOff,  string trafficeLightOn )
    {
        
        var option = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        
        var data = new RootSendMessages
        {
            timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            messages = new List<sendMessages>()
        };
        
        
        foreach (var item in trafficeLightOff)
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

        foreach (var item in trafficeLightOn)
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
        

        string json = JsonSerializer.Serialize(data, option);
        return json;
    }
    

  
    
}