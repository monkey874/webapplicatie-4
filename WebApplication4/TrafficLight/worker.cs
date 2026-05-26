using System.Text.Json;
using WebApplication4.TrafficLight.sort;
namespace WebApplication4.TrafficLight;

public class Worker
{
    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("taak word uitgevoerd");
            if (taskManeger.Queue.TryDequeue(out var task))
            {
                
                var jsonDoc = JsonDocument.Parse(task);
                var data = jsonDoc.RootElement;
               

                var post = new PostRequest();
                var ontvangen = new Sorter();
                var (on, of) = Sorter.Laod(data);
                
                
                foreach (var I in of)
                {
                    var (
                        trafficLightsNames1,
                        TrafficLightGreenTime,
                        trafficLightsRelationshipOff,
                        trafficeLightsRelationshipOn
                        ) = Sorter.GeneratorSort(data, I);

                    var trafficeLightOff = trafficLightsRelationshipOff.ToArray();
                    var trafficeLightOn = trafficeLightsRelationshipOn.ToArray();
                    

                    var json = CreateJson.GenerateJsonStructure(trafficeLightOff, trafficeLightOn);
                    
                    var obj = JsonSerializer.Deserialize<object>(json);
                    var pretty = JsonSerializer.Serialize(obj, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                    
                    Console.WriteLine(pretty);
                    var result = await post.SendAsync("http://172.16.48.244:5050/receive", pretty);

                    
                    Console.WriteLine("GreenTime gevonden: " + TrafficLightGreenTime[0]);
                    await Task.Delay(TrafficLightGreenTime[0] * 1000);
                    
                    
                    
                    var json1  = CreateJson.trafficLightOff(trafficeLightOn);
                    var obj1 = JsonSerializer.Deserialize<object>(json1);
                    var pretty1 = JsonSerializer.Serialize(obj1, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
                    var result1 = await post.SendAsync("http://172.16.48.244:5050/receive", pretty1);
                    await Task.Delay(10000);
                }
            }
            else
            {
                await Task.Delay(1000);
            }
        }
    }
}