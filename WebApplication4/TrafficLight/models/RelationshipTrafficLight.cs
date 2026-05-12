using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Stoplichtsysteem.models;


    public class RootRelationshipTrafficeLight   
    {
        public required List<RelationshipTrafficLight> RelationshipTrafficLights { get; init; }
    }

    public class RelationshipTrafficLight
    {
        [JsonPropertyName("traffic light")]
        
        public required double TrafficLight { get; set; }
        public required List<double> RelationshipOff { get; set; }
        public required List<double> RelationshipOn  {get; set;}
    }
    