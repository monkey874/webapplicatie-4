using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using WebApplication4.TrafficLight.sort;
using WebApplication4.TrafficLight;
using WebApplication4;
namespace WebApplication4.Controller;

[ApiController]
[Route("[controller]")]
public class ReceiveController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Receive([FromBody] object data)

    {
        
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
        string result = await post.SendAsync("http://172.16.48.79:5280/receive", pretty);
        
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

