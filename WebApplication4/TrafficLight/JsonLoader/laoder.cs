namespace WebApplication4.TrafficLight.JsonLoader
{
    public class JsonLoader
    {
        public static string LoadJsonFiles(string jsonFile)
        {
            var json = File.ReadAllText(jsonFile);
            return json;
        }
    }
}