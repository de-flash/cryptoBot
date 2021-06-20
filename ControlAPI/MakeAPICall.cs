using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoBot.ControlAPI
{
    public static class MakeApiCall
    {
        public static async Task<JArray> GetTickerData(string id, string convert = "USD", string interval = "1h")
        {
            var url = new UriBuilder("https://api.nomics.com/v1/currencies/ticker");
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["key"] = await File.ReadAllTextAsync
                (@"C:\Users\majus\RiderProjects\CryptoBot\CryptoBot\ControlAPI\apikey.txt");
            queryString["ids"] = id.ToUpper();
            queryString["interval"] = interval.ToLower();
            queryString["convert"] = convert.ToUpper();
            url.Query = queryString.ToString();

            try
            {
                var client = new WebClient();
                var jsonString = await client.DownloadStringTaskAsync(url.ToString());
                return JsonConvert.DeserializeObject<JArray>(jsonString);
            }
            catch (Exception)
            {
                return new JArray();
            }
        }

        public static async Task<JArray> GetMetaData(string id)
        {
            var url = new UriBuilder("https://api.nomics.com/v1/currencies");
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["key"] = await File.ReadAllTextAsync
                (@"C:\Users\majus\RiderProjects\CryptoBot\CryptoBot\ControlAPI\apikey.txt");
            queryString["ids"] = id.ToUpper();
            url.Query = queryString.ToString();
            
            try
            {
                var client = new WebClient();
                var jsonString = await client.DownloadStringTaskAsync(url.ToString());
                return JsonConvert.DeserializeObject<JArray>(jsonString);
            }
            catch (Exception)
            {
                return new JArray();
            }
        }
    }
}