namespace WebApplication4.Controller;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using WebApplication4.TrafficLight.sort;
using WebApplication4.TrafficLight;
using WebApplication4;
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
        Console.WriteLine("gebeurt er iets");
        var post = new PostRequest();
     
        var ontvangen = new sorter();
        var (of, on ) = ontvangen.Laod(data);
        
        double[] trafficeLightOff = of.ToArray();
        double[] trafficeLightOn = on.ToArray();
        var test = new createJson();
        
        string json = test.generateJsonStructure(trafficeLightOff, trafficeLightOn);
        
        var obj = JsonSerializer.Deserialize<object>(json);
        var pretty = JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        
        Console.WriteLine(pretty);
        string result = await post.SendAsync("http://172.16.48.58:5501/post", pretty);
        
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