namespace WebApplication4.TrafficLight.models;
public class RootGetMessages
{
    public required List<GetMessages> messages { get; init; }
    public  string? timestamp { get; init; }
}

public class GetMessages
{
    public required string type {get; init;}
    public required getMessageContent message { get; init; }
    
}
public class getMessageContent
{
    public required string id { get; init; }
    public string? expectedArrival { get; set; }
}