using Newtonsoft.Json.Linq;

namespace APIsAndJSON
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Use a for loop to generate at least five quotes from each api and have them alternate to simulate a conversation
            for (int i = 0; i < 5; i++)
            {
                RonVSKanyeAPI ronVSKanye = new RonVSKanyeAPI();
            }

            OpenWeatherMapAPI openWeatherMap = new OpenWeatherMapAPI();
        }
    }
}
