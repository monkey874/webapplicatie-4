using System.Text.Json;
using System.Text;
using System.Text.Json;
using WebApplication4.TrafficLight.sort;

namespace WebApplication4.TrafficLight;

public class Worker
{
    // Eén gedeelde HttpClient voorkomt socket-uitputting
    private static readonly HttpClient client = new HttpClient
    {
        Timeout = TimeSpan.FromSeconds(5)
    };

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("taak wordt uitgevoerd");
            if (!taskManeger.Queue.TryDequeue(out var task) || string.IsNullOrWhiteSpace(task))
            {
                await Task.Delay(10000);
                continue;
            }
            
                
            
            JsonDocument jsonDoc;
            try
            {
                jsonDoc = JsonDocument.Parse(task);
            }
            catch
            {
                Console.WriteLine("Ongeldige JSON ontvangen");
                continue;
            }

            
            var data = jsonDoc.RootElement;
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
                
                var pretty = JsonSerializer.Serialize(
                    JsonSerializer.Deserialize<object>(json),
                    new JsonSerializerOptions { WriteIndented = true }
                );

                Console.WriteLine(pretty);

                if ("bas" == "as")
                {
                    taskManeger2.Queue1.Enqueue(pretty);

                    // GET request met timeout + foutafhandeling
                    try
                    {
                        var response = await client.GetAsync("http://172.16.48.224:5280/recieve");

                        if (!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Server gaf geen geldige response");
                           
                        }

                        string text = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(text);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Fout bij GET: " + ex.Message);
                       
                    }
                    
                

                    // Wachttijd groen licht
                    Console.WriteLine("GreenTime gevonden: " + TrafficLightGreenTime[0]);
                    await Task.Delay(TrafficLightGreenTime[0] * 1000);

                    // Licht uit JSON
                    var json1 = CreateJson.trafficLightOff(trafficeLightOn);

                    try
                    {
                        await client.GetAsync(
                            "http://172.16.48.244:5280/receive");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Fout bij POST: " + ex.Message);
                    }

                    await Task.Delay(10000);
                }
                else
                {
                    var post = new PostRequest();

// Eerste POST
                    var objOn = JsonSerializer.Deserialize<object>(
                        CreateJson.GenerateJsonStructure(trafficeLightOff, trafficeLightOn)
                    );
                    var result = await post.SendAsync("http://192.168.2.8:5501/post", objOn);

                    Console.WriteLine("GreenTime gevonden: " + TrafficLightGreenTime[0]);
                    await Task.Delay(TrafficLightGreenTime[0] * 1000);

// Tweede POST
                    var objOff = JsonSerializer.Deserialize<object>(
                        CreateJson.trafficLightOff(trafficeLightOn)
                    );
                    var result1 = await post.SendAsync("http://192.168.2.8:5501/post", objOff);

                    



        
                }
            } 
        }
    }
}
