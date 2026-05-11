namespace WebApplication4;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication4;
using System.IO;


[ApiController]
[Route("[controller]")]
public class testController : ControllerBase
{
    [HttpPost]
    public IActionResult Receive([FromBody] object data)
    {
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
        var jsondata = new onthoud_json();
        string test = jsondata.readFile();
        return Ok(new { test });
    }
}