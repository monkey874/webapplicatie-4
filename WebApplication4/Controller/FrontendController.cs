using Microsoft.AspNetCore.Mvc;
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
}