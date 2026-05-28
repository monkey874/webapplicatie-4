using System.Text.Json.Serialization;
namespace WebApplication4.TrafficLight.models;


    public class RootSendMessages
    {
        public required List<sendMessages> messages { get; init; }
        public required string? timestamp  { get; init; }
    }

    public class sendMessages
    {
        [JsonPropertyName("type")]
        public  required string type  { get; init; }
        public  required sendMessageContent message  { get; init; } 
    }

    public class sendMessageContent
    {
        public required string id { get; init; }
        public required string state { get; init; }
    }
