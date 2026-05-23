using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using WebApplication4.TrafficLight.models;


namespace WebApplication4.TrafficLight.sort;

public class Sorter
{
    public static (List<string> trafficLightNames, List<string> volgordeTrafficeLight) Laod(object incomingJsonobject)
    {
        var incomingjsonobject = incomingJsonobject.ToString();
        Debug.Assert(incomingjsonobject != null, nameof(incomingjsonobject) + " != null");
        var data = JsonSerializer.Deserialize<RootGetMessages>(incomingjsonobject);
        

        List<string> typeVehicle = [];
        List<string> trafficeLightId = [];
   
        if (data?.messages != null)
            foreach (var i in data.messages)
            {
                typeVehicle.Add(i.type);
                trafficeLightId.Add(i.message.id);
            }


        var test = trafficeLightId.ToArray();
        List<int> aantalvechilesfortrafficeLight = [];
        List<string> trafficeLightName = [];
        foreach (var i in test)
        {
            var searchverb = i;
            var aantal = test.Count(w => w == searchverb);
            if (trafficeLightName.Contains(searchverb)) continue;
            aantalvechilesfortrafficeLight.Add(aantal);
            trafficeLightName.Add(searchverb);
        }

        List<string> volgordeTrafficeLight = [];
        while (aantalvechilesfortrafficeLight.Count > 0)
        {
            var n = aantalvechilesfortrafficeLight.Max();
            var index = Array.IndexOf(aantalvechilesfortrafficeLight.ToArray(), n);
            var trafficlight = trafficeLightName[index];
            aantalvechilesfortrafficeLight.Remove(n);
            trafficeLightName.RemoveAt(index);
            volgordeTrafficeLight.Add(trafficlight);
        }
        
        var relation = JsonLoader.JsonLoader.LoadJsonFiles("relation.json");
        var jsonfile = JsonSerializer.Deserialize<RootRelationshipTrafficeLight>(relation);
        
        List<string> trafficLightNames = [];

        if (jsonfile?.RelationshipTrafficLights == null) return (trafficLightNames, volgordeTrafficeLight);
        foreach (var trafficLightRelationship in jsonfile.RelationshipTrafficLights)
        {
            var nameTrafficLightDouble = trafficLightRelationship.TrafficLight;
            var nameTrafficLight = nameTrafficLightDouble.ToString(CultureInfo.InvariantCulture);
            trafficLightNames.Add(nameTrafficLight.Replace(",", "."));
        }
        return (trafficLightNames, volgordeTrafficeLight);
    }
    
    public static (List<double>trafficLightsNames, List<int>TrafficLightGreenTime, List<double>trafficLightsRelationshipOff, List<double> trafficeLightsRelationshipOn) GeneratorSort(object jsonFile, string trafficid){
        
        List<double> trafficLightsNames = [];
        List<int> trafficLightGreenTime = [];
        List<double> trafficeLightsRelationshipOn = [];
        List<double> trafficLightsRelationshipOff = [];
        
        var relation = JsonLoader.JsonLoader.LoadJsonFiles("relation.json");
        var jsonfile = JsonSerializer.Deserialize<RootRelationshipTrafficeLight>(relation);
        

        var group = jsonfile?.RelationshipTrafficLights.FirstOrDefault(x => x.TrafficLight == double.Parse(trafficid, CultureInfo.InvariantCulture));


        if (group == null)
            return (
                trafficLightsNames,
                trafficLightGreenTime,
                trafficLightsRelationshipOff,
                trafficeLightsRelationshipOn
            );
        trafficLightsNames.Add(group.TrafficLight);
        trafficLightGreenTime.Add(group.TrafficLightGreenTime);
        trafficeLightsRelationshipOn.AddRange(group.RelationshipOn);
        trafficLightsRelationshipOff.AddRange(group.RelationshipOff);


        return (
            trafficLightsNames, 
            trafficLightGreenTime,
            trafficLightsRelationshipOff, 
            trafficeLightsRelationshipOn
        );
    }
}