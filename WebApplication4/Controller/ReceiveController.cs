using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controller;

[ApiController]
[Route("[controller]")]
public class ReceiveController : ControllerBase
{
    [HttpPost]
    public IActionResult Receive([FromBody] object data)
    {
        Console.WriteLine("gebeurt er iets 1");
        Console.WriteLine("Ontvangen JSON:");
        Console.WriteLine(data);
        var jsondata = new onthoud_json();
        string jsonData = data.ToString();
        jsondata.riteFile(jsonData);
        return Ok(new { status = "received", data });
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

