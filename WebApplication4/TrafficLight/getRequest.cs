namespace WebApplication4.TrafficLight;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class ApiClient
{
    private readonly HttpClient _client;

    public ApiClient()
    {
        _client = new HttpClient();
    }

    public async Task<string?> GetDataAsync(string url)
    {
        try
        {
            Console.WriteLine("⏳ GET request sturen...");

            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"❌ Server fout: {(int)response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }

            string responseText = await response.Content.ReadAsStringAsync();

            Console.WriteLine("✅ Server antwoord ontvangen!");
            return responseText;
        }
        catch (Exception ex)
        {
            Console.WriteLine("⛔ Fout:");
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}