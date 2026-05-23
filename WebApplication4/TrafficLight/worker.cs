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

                    var test = new CreateJson();

                    var json = CreateJson.GenerateJsonStructure(trafficeLightOff, trafficeLightOn);
                    Console.WriteLine(json);
                    var obj = JsonSerializer.Deserialize<object>(json);
                    var pretty = JsonSerializer.Serialize(obj, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                    
                    Console.WriteLine(pretty);
                    var result = await post.SendAsync("172.16.48.188", pretty);


                    await Task.Delay(TrafficLightGreenTime[0] * 1000);
                }
            }
            else
            {
                await Task.Delay(1000);
            }
        }
    }
}