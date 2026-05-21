namespace WebApplication4.TrafficLight.models;
public class RootGetMessages
{
    public required List<GetMessages> Messages { get; init; }
    public required string Timestamp { get; init; }
}

public class GetMessages
{
    public required string type {get; init;}
    public required getMessageContent message { get; init; }
    
}
public class getMessageContent
{
    public required string Id { get; init; }
    public string? ExpectedArrival { get; set; }
}