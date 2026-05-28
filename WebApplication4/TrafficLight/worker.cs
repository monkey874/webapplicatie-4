using System.Text.Json;
using System.Text;
using System.Text.Json;
using WebApplication4.TrafficLight.sort;

namespace WebApplication4.TrafficLight;

public class Worker
{
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
                
            }
            
                
            
            JsonDocument jsonDoc;
            try
            {
                Console.WriteLine(task);
                jsonDoc = JsonDocument.Parse(task);
            }
            catch
            {
                Console.WriteLine("Ongeldige JSON ontvangen");
                continue;
            }
            

            
            var data = jsonDoc.RootElement;
            Console.WriteLine("dit is data");
            Console.WriteLine(data);
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

              var json = train(trafficLightsNames1.ToArray(), trafficeLightOff, trafficeLightOn);

                
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
                        json
                    );
                    var result = await post.SendAsync("http://172.16.48.223:5501/post", objOn);

                    Console.WriteLine("GreenTime gevonden: " + TrafficLightGreenTime[0]);
                    await Task.Delay(TrafficLightGreenTime[0] * 1000);

// Tweede POST      
                    if(trafficLightsNames1[0] != "train-1")
                    {
                        
                    var objOff = JsonSerializer.Deserialize<object>(
                        CreateJson.trafficLightOff(trafficeLightOn)
                    );
                    var result1 = await post.SendAsync("http://172.16.48.223:5501/post", objOff);
                    }
                    else
                    {
                        Console.WriteLine("ik ben de trein");

                        var objOff = JsonSerializer.Deserialize<object>(
                            CreateJson1.trainJsonopeningCate(trafficLightsNames1[0])
                        );
                        var result1 = await post.SendAsync("http://172.16.48.223:5501/post", objOff);
                    }
                }
            } 
        }
    }

    public string train(string[] trafficLightsNames1, double[] trafficeLightOff, double[] trafficeLightOn)
    {
        if (trafficLightsNames1[0] != "train-1")
        {
            var json = CreateJson.GenerateJsonStructure(trafficeLightOff, trafficeLightOn);
            return json;
        }
        else
        {
            Console.WriteLine("er komt een trein");
            var json = CreateJson1.trainJsonClossingCate(trafficLightsNames1[0]);
            Console.WriteLine("dit is de json" + json);
            return json;
        }
    }

    
}
