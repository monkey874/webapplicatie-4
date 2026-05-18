using System.Globalization;
using System.Text.Json;
using WebApplication4.TrafficLight;
using System;
using System.Collections.Generic;
using WebApplication4.TrafficLight.models;

namespace WebApplication4.TrafficLight.sort;

public class sorter
{
    public (List<double> off, List<double> on) Laod(object incomingJsonobject)
    {
        var incomingjsonobject = incomingJsonobject.ToString();
        RootGetMessages data = JsonSerializer.Deserialize<RootGetMessages>(incomingjsonobject);
        

        List<string> typeVehicle = new List<string>();
        List<string> trafficeLightId = new List<string>();
        List<string> expectedArrival = new List<string>();  
        
        
        foreach (var i in data.Messages)
        {
            typeVehicle.Add(i.Type);
            trafficeLightId.Add(i.Message.Id);
            expectedArrival.Add(i.Message.ExpectedArrival);
        }
        

        var defineClass = new vehiclePriority();

        Console.Clear();
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

        List<double> trafficeLightsOn = new List<double>();
        List<double> trafficeLightsOff = new List<double>();

        for (int i = 0; i < volgordeTrafficeLight.Count; i++)
        {
            if (trafficLightNames.Contains(volgordeTrafficeLight[i]))
            {
                double trafficlightid = double.Parse(volgordeTrafficeLight[i], CultureInfo.InvariantCulture);

                var group = jsonfile.RelationshipTrafficLights.FirstOrDefault(x => x.TrafficLight == trafficlightid);
                if (group != null)
                {
                    trafficeLightsOn.AddRange(group.RelationshipOn);
                    trafficeLightsOff.AddRange(group.RelationshipOff);
                }
            }
        }
        return (trafficeLightsOff, trafficeLightsOn);
    }
    

  
}