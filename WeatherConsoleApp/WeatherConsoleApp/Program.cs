using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        var apiKey = "25fa78f182f946358d5120720250804"; // <-- Put your WeatherAPI key here
        var city = "Ljubljana";      // Or any city
        var url = $"https://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={city}&days=1";

        using var client = new HttpClient();
        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Failed to get weather data.");
            return;
        }

        var json = await response.Content.ReadAsStringAsync();
        var data = JObject.Parse(json);

        // Get location information
        var location = data["location"]["name"];
        var country = data["location"]["country"];

        // Get current weather data
        var currentTemp = data["current"]["temp_c"];
        var condition = data["current"]["condition"]["text"];
        var icon = data["current"]["condition"]["icon"];

        // Get forecast data for the day
        var maxTemp = data["forecast"]["forecastday"][0]["day"]["maxtemp_c"];
        var minTemp = data["forecast"]["forecastday"][0]["day"]["mintemp_c"];

        // Display the results
        Console.WriteLine($"📍 Location: {location}, {country}");
        Console.WriteLine($"🌤 Current Condition: {condition}");
        Console.WriteLine($"🌡 Current Temp: {currentTemp}°C");
        Console.WriteLine($"🌡 Max Temp: {maxTemp}°C");
        Console.WriteLine($"🌡 Min Temp: {minTemp}°C");
        Console.WriteLine($"🖼 Icon URL: https:{icon}");
    }
}
