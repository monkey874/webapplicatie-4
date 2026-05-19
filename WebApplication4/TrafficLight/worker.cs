using System.Text.Json;
using WebApplication4.TrafficLight.sort;
namespace WebApplication4.TrafficLight;

public class worker
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
                var ontvangen = new sorter();
                var (on, of) = ontvangen.Laod(data);
                Console.WriteLine("vanaf hier");
                foreach (var I in of)
                {
                    var (
                        trafficLightsNames1,
                        TrafficLightGreenTime,
                        trafficLightsRelationshipOff,
                        trafficeLightsRelationshipOn
                        ) = ontvangen.generatorSort(data, I);

                    double[] trafficeLightOff = trafficLightsRelationshipOff.ToArray();
                    double[] trafficeLightOn = trafficeLightsRelationshipOn.ToArray();

                    var test = new createJson();

                    string json = test.generateJsonStructure(trafficeLightOff, trafficeLightOn);

                    var obj = JsonSerializer.Deserialize<object>(json);
                    var pretty = JsonSerializer.Serialize(obj, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                    Console.WriteLine(pretty);
                    string result = await post.SendAsync("http://172.16.48.5dd8:5501/post", pretty);


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