using System.Text.Json.Serialization;

namespace WebApplication4.TrafficLight.models;


    public class RootSendMessages
    {
        public required List<sendMessages> Messages { get; init; }
        public required string Timestamp  { get; init; }
    }

    public class sendMessages
    {
        [JsonPropertyName("Type")]
        public  required string Type  { get; init; }
        public  required sendmessageContent Message  { get; init; } 
    }

    public class sendmessageContent
    {
        public required double Id { get; init; }
        public required string State { get; init; }
    }
