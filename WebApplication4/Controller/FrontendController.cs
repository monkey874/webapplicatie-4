using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

using WebApplication4.TrafficLight;

namespace WebApplication4.Controller;

[ApiController]
[Route("[controller]")]
public class recieveController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Receive([FromBody] object data)
    {
        taskManeger.Queue.Enqueue(data.ToString());
        return Ok(new { status = "recieved"  });
    }
    
    [HttpGet]
    public IActionResult GetStatus()
    {
        // Probeer een taak uit de queue te halen
        if (!taskManeger2.Queue1.TryDequeue(out var task) || string.IsNullOrWhiteSpace(task))
        {
            return Ok(new { status = "geen data beschikbaar" });
        }

        // Probeer JSON te parsen
        try
        {
            using var jsonDoc = JsonDocument.Parse(task);
            var data = jsonDoc.RootElement.Clone(); // Clone zodat JsonDocument kan disposen

            return Ok(data);
        }
        catch (JsonException)
        {
            return BadRequest(new { error = "Ongeldige JSON ontvangen", raw = task });
        }
    }

}
