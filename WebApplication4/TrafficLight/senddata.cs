using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System;

namespace WebApplication4.TrafficLight;

public class PostRequest
{
    private readonly HttpClient _client;

    public PostRequest()
    {
        _client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(5)
        };
    }

    public async Task<string?> SendAsync(string url, object payload)
    {
        try
        {
            Console.WriteLine("⏳ Versturen van POST request...");

            string json = JsonSerializer.Serialize(payload);

            var response = await _client.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"❌ Server fout: {(int)response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }

            string responseText = await response.Content.ReadAsStringAsync();

            Console.WriteLine("✅ Server antwoord ontvangen!");
            return responseText;
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("⛔ Timeout: server reageert niet.");
            return null;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("⛔ Verbindingsfout:");
            Console.WriteLine(ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("⛔ Onbekende fout:");
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}