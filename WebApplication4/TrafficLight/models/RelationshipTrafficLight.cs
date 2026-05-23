using System.Text.Json.Serialization;
namespace WebApplication4.TrafficLight.models;


    public class RootRelationshipTrafficeLight   
    {
        public required List<RelationshipTrafficLight> RelationshipTrafficLights { get; init; }
    }

    
    public class RelationshipTrafficLight
    {
        [JsonPropertyName("traffic light")]
        
        public required double TrafficLight { get; set; }
        
        [JsonPropertyName("TrafficLightGreenTime")]
        public int TrafficLightGreenTime { get; set; }
        
        public required List<double> RelationshipOff { get; set; }
        public required List<double> RelationshipOn  {get; set;}
    }
    