using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace WebApplication4.TrafficLight.models;


    public class RootSendMessages
    {
        public required List<sendMessages> messages { get; init; }
        public required string timestamp  { get; init; }
    }

    public class sendMessages
    {
        [JsonPropertyName("Type")]
        public  required string type  { get; init; }
        public  required sendmessageContent message  { get; init; } 
    }

    public class sendmessageContent
    {
        public required double id { get; init; }
        public required string state { get; init; }
    }
