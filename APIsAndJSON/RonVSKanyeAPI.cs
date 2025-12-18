using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace APIsAndJSON
{
    internal class RonVSKanyeAPI
    {
        public RonVSKanyeAPI()
        {
            HttpClient client = new HttpClient();

            string kanyeURL = EncapsulateURL.GetKanyeURL();
            string kanyeResponse = client.GetStringAsync(kanyeURL).Result;
            // Use JObject to parse JSON objects
            string kanyeQuote = JObject.Parse(kanyeResponse).GetValue("quote").ToString();

            string ronURL = EncapsulateURL.GetRonURL();
            string ronResponse = client.GetStringAsync(ronURL).Result;
            // Use JArray to parse JSON arrays
            string ronQuote = JArray.Parse(ronResponse).ToString().Replace('[', ' ').Replace(']', ' ').Trim();
            
            Console.WriteLine($"Kanye West: {kanyeQuote}\nRon Swanson: {ronQuote}");
        }
    }

    internal static class EncapsulateURL
    {
        private static string _kanyeURL = "https://api.kanye.rest";

        public static string GetKanyeURL()
        {
            string kanyeURL = _kanyeURL;
            return kanyeURL;
        }

        private static string _ronURL = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";

        public static string GetRonURL()
        {
            string ronURL = _ronURL;
            return ronURL;
        }
    }
}
