using WebApplication4.TrafficLight.Timer;

namespace WebApplication4.Controller;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using WebApplication4.TrafficLight.sort;
using WebApplication4.TrafficLight;
using WebApplication4;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using WebApplication4;
using System.IO;
using System;
using WebApplication4.TrafficLight;


[ApiController]
[Route("[controller]")]
public class testController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Receive([FromBody] object data)
    {
        taskManeger.Queue.Enqueue(data.ToString());
        
        Console.WriteLine("gebeurt er iets");
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

            var klok = new TrafficLightTimer();
            klok.setTimer(TrafficLightGreenTime[0]);
            Console.WriteLine("de klok is al afgegaan");
        }
            

       
        return Ok(new { status = "received"  });
    }

   
    [HttpGet]
    public IActionResult GetStatus()
    {
        Console.WriteLine("gebeurt er iets");
        var jsondata = new onthoud_json();
        string test = jsondata.readFile();
        return Ok(new { test });
    }
}