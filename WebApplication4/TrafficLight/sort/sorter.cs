using System.Globalization;
using System.Text.Json;
using WebApplication4.TrafficLight.models;

namespace WebApplication4.TrafficLight.sort;

public class sorter
{
    public (List<string> trafficLightNames, List<string> volgordeTrafficeLight) Laod(object incomingJsonobject)
    {
        var incomingjsonobject = incomingJsonobject.ToString();
        RootGetMessages data = JsonSerializer.Deserialize<RootGetMessages>(incomingjsonobject);


        List<string> typeVehicle = new List<string>();
        List<string> trafficeLightId = new List<string>();
        List<string> expectedArrival = new List<string>();


        foreach (var i in data.Messages)
        {
            typeVehicle.Add(i.type);
            trafficeLightId.Add(i.message.Id);
            expectedArrival.Add(i.message.ExpectedArrival);
        }


        var defineClass = new vehiclePriority();

        
        foreach (var i in (typeVehicle))
        {
            defineClass.checkVehiclePriority(i);
            Console.WriteLine(i);
        }

        string[] test = trafficeLightId.ToArray();
        List<int> aantalvechilesfortrafficeLight = new List<int>();
        List<string> trafficeLightName = new List<string>();
        foreach (var i in test)
        {
            string searchverb = i;
            int aantal = test.Count(w => w == searchverb);
            if (!trafficeLightName.Contains(searchverb))
            {
                aantalvechilesfortrafficeLight.Add(aantal);
                trafficeLightName.Add(searchverb);
            }
        }

        List<string> volgordeTrafficeLight = new List<string>();
        while (aantalvechilesfortrafficeLight.Count > 0)
        {
            int n = aantalvechilesfortrafficeLight.Max();
            int index = Array.IndexOf(aantalvechilesfortrafficeLight.ToArray(), n);
            string trafficlight = trafficeLightName[index];
            aantalvechilesfortrafficeLight.Remove(n);
            trafficeLightName.RemoveAt(index);
            volgordeTrafficeLight.Add(trafficlight);
        }

        var loader1 = new JsonLoader.JsonLoader();
        var relation = loader1.LoadJsonFiles("relation.json");
        RootRelationshipTrafficeLight jsonfile = JsonSerializer.Deserialize<RootRelationshipTrafficeLight>(relation);


        


        List<string> trafficLightNames = [];

        foreach (var trafficLightRelationship in jsonfile.RelationshipTrafficLights)
        {
            var nameTrafficLightDouble = trafficLightRelationship.TrafficLight;
            var nameTrafficLight = nameTrafficLightDouble.ToString();
            trafficLightNames.Add(nameTrafficLight.Replace(",", "."));
        }

        return (trafficLightNames, volgordeTrafficeLight);

    }
    
    public (List<double>trafficLightsNames, List<int>TrafficLightGreenTime, List<double>trafficLightsRelationshipOff, List<double> trafficeLightsRelationshipOn) generatorSort(object Jsonfile, string trafficid){
        
        List<double> trafficLightsNames = [];
        List<int> trafficLightGreenTime = [];
        List<double> trafficeLightsRelationshipOn = [];
        List<double> trafficLightsRelationshipOff = [];

        var data = Laod(Jsonfile);
        
        var loader = new JsonLoader.JsonLoader();
        var relation = loader.LoadJsonFiles("relation.json");
        var jsonfile = JsonSerializer.Deserialize<RootRelationshipTrafficeLight>(relation);
        

        var group = jsonfile?.RelationshipTrafficLights.FirstOrDefault(x => x.TrafficLight == double.Parse(trafficid, CultureInfo.InvariantCulture));
            
    
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