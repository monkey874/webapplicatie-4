using System.Text.Json.Serialization;
namespace WebApplication4.TrafficLight.models;

public class GetVehicleInfo
{
    [JsonPropertyName("vehicleInfo")]
    public required List<VehicleInfo> VehicleInfo  { get; init; }
}
public class VehicleInfo
{
    [JsonPropertyName("vehicleName")]
    public required string vehicleName { get; init; }
    [JsonPropertyName("vehiclePriority")]
    public required string vehiclePriority {get; init; }
}