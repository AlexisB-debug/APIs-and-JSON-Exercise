using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace APIsAndJSON
{
    internal class OpenWeatherMapAPI
    {
        public OpenWeatherMapAPI()
        {
            HttpClient client = new HttpClient();
            string APIKey = JObject.Parse(File.ReadAllText("appsettings.json")).GetValue("APIKey")?.ToString() ?? "";
            Console.WriteLine("Please, choose a city to receive the weather & forecast!");
            string city = Console.ReadLine() ?? "";

            // &units=imperial delivers the temperature in degrees Fahrenheit
            // Imperial units of measurement are not available for snow & rain.
            string apiCallWeather =
                $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={APIKey}&units=imperial";

            JObject weather = JObject.Parse(client.GetStringAsync(apiCallWeather).Result);
            string cityName = weather["name"]?.ToString() ?? "Unknown";
            string country = weather["sys"]?["country"]?.ToString() ?? "Unknown";
            string latitude = weather["coord"]?["lat"]?.ToString() ?? "Unknown";
            string longitude = weather["coord"]?["lon"]?.ToString() ?? "Unknown";
            string timezone = weather["timezone"]?.ToString() ?? "Unknown";
            string sunrise = weather["sys"]?["sunrise"].ToString() ?? "Unknown";
            string sunset = weather["sys"]?["sunset"].ToString() ?? "Unknown";
            string main = weather["weather"]?[0]?["main"]?.ToString() ?? "Unknown";
            string temperature = weather["main"]?["temp"]?.ToString() ?? "Unknown";
            string rain = weather["rain"]?["1h"]?.ToString() ?? "0";
            string snow = weather["main.snow"]?["1h"]?.ToString() ?? "0";
            string humidity = weather["main"]?["humidity"]?.ToString() ?? "Unknown";
            string wind = weather["wind"]?["speed"]?.ToString() ?? "Unknown";
            string clouds = weather["clouds"]?["all"]?.ToString() ?? "Unknown";
            string visibility = weather["visibility"]?.ToString() ?? "Unknown";

            Console.WriteLine(cityName + ", " + country + " Geographic Coordinate:" + " " + latitude + ", " +
                              longitude + " Time Zone: " + timezone + " Sunrise: " + sunrise + " Sunset: " + sunset +
                              " Weather: " + main + " Temperature Fahrenheit: " + temperature + " Humidity: " +
                              humidity + " percent" + " Clouds: " + clouds + " percent" + " Visibility: " + visibility +
                              " meters" + " Wind: " + wind + " miles/hour" + " Rain: " + rain + " millimeters" +
                              " Snow: " + snow + " millimeters");

            // &units=imperial delivers the temperature in degrees Fahrenheit
            string apiCallForecast =
                $"http://api.openweathermap.org/data/2.5/forecast?q={city}&appid={APIKey}&units=imperial";

            JObject forecast = JObject.Parse(client.GetStringAsync(apiCallForecast).Result);
            string forecastCityName = forecast["city"]["name"].ToString();
            string forecastCountry = forecast["city"]["country"].ToString();
            string forecastLatitude = forecast["city"]["coord"]["lat"].ToString();
            string forecastLongitude = forecast["city"]["coord"]["lon"].ToString();
            string forecastTimezone = forecast["city"]["timezone"].ToString();
            string forecastSunrise = forecast["city"]["sunrise"].ToString();
            string forecastSunset = forecast["city"]["sunset"].ToString();

            // The OpenWeatherMap 'five-day weather forecast' collects data in three-hour steps,
            // which is eight data points per twenty-four hours or forty entries.
            for (int i = 0; i < 40; i++)
            {
                string forecastDateTime = forecast["list"]?[i]?["dt_txt"]?.ToString() ?? "Unknown";
                string forecastMain = forecast["list"]?[i]?["weather"]?[0]?["main"]?.ToString() ?? "Unknown";
                string forecastTemperature = forecast["list"]?[i]?["main"]?["temp"]?.ToString() ?? "Unknown";
                string forecastHumidity = forecast["list"]?[i]?["main"]?["humidity"]?.ToString() ?? "Unknown";
                string forecastClouds = forecast["list"]?[i]?["clouds"]?["all"]?.ToString() ?? "Unknown";
                string forecastVisibility = forecast["list"]?[i]?["visibility"]?.ToString() ?? "Unknown";
                string forecastWind = forecast["list"]?[i]?["wind"]?["speed"]?.ToString() ?? "Unknown";
                string forecastRain = forecast["list"]?[i]?["rain"]?["3h"]?.ToString() ?? "0";
                string forecastSnow = forecast["list"]?[i]?["snow"]?["3h"]?.ToString() ?? "0";
                Console.WriteLine("City, Country: " + forecastCityName + ", " + forecastCountry + "\n" +
                                  " Geographic Coordinate: " + forecastLatitude + ", " + forecastLongitude + "\n" +
                                  " Time Zone: " + forecastTimezone + " Sunrise: " + forecastSunrise + " Sunset: " +
                                  forecastSunset + " Date & Time: " + forecastDateTime + "\n" + " Weather: " +
                                  forecastMain + " Temperature Fahrenheit: " + forecastTemperature + " Humidity: " +
                                  forecastHumidity + " percent" + " Clouds: " + forecastClouds + " percent" +
                                  " Visibility: " + forecastVisibility + " meters" + " Wind: " + forecastWind +
                                  " miles/hour" + " Rain: " + forecastRain + " millimeters" + " Snow: " + forecastSnow +
                                  " millimeters");
            }
        }
    }
}
