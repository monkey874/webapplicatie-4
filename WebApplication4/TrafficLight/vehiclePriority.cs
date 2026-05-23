namespace WebApplication4.TrafficLight;
using WebApplication4.TrafficLight.models;
using System.Collections.Generic;
using System;
using System.Text.Json;

public class vehiclePriority
{
    private (string[] vehiclePriorityList, string[] vehiclesnamesArray ) LoadVehiclePriority()
    {
        var test = new JsonLoader.JsonLoader();
        var vehicleInfo = JsonLoader.JsonLoader.LoadJsonFiles("vechileInfo.json");
        GetVehicleInfo data = JsonSerializer.Deserialize<GetVehicleInfo>(vehicleInfo);

        List<string> vehiclesnames = new List<string>();
        List<string> vehiclesPriority = new List<string>();
        
        foreach (var i in data?.VehicleInfo)
        {
            vehiclesnames.Add(i.vehicleName);
            vehiclesPriority.Add(i.vehiclePriority);
        }
        var vehiclesnamesArray = vehiclesnames.ToArray();
        var vehiclesPriorityArray = vehiclesPriority.ToArray();

        return (vehiclesPriorityArray, vehiclesnamesArray);
    }
        
    public void checkVehiclePriority(string vehicleName)
    {
        var testvehicle = vehicleName;
        var vehicleInfo = LoadVehiclePriority();
        var vehicleInfoArray = vehicleInfo.vehiclesnamesArray;
        var VehicleInofArrayPriority = vehicleInfo.vehiclePriorityList;
        if (vehicleInfoArray.Contains(testvehicle))
        {
            var index = vehicleInfoArray.ToList().IndexOf(testvehicle);
            var vehiclePriority = VehicleInofArrayPriority[index];
            var vehicleNameItem =  vehicleInfoArray[index];
            
            Console.WriteLine(vehiclePriority);
            Console.WriteLine(vehicleNameItem);
        }
    }
}