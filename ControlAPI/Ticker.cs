using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CryptoBot.Helper;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace CryptoBot.ControlAPI
{
    public class Ticker : ModuleBase<SocketCommandContext>
    {
        public static async Task CheckAlerts
            (DiscordSocketClient client, string ids = "ETH,BTC", string interval = "1h", string currency = "USD")
        {
            if (ids.Length == 0) return;
            try
            {
                var data = MakeApiCall.GetTickerData(ids.Remove(ids.Length-1), currency, interval).Result;
                
                foreach (var alert in Globals.AlertList.ToList())
                {
                    var currentPrice = double.Parse(data
                        .First(token => token.Value<string>("id") == alert.Currency)
                        .Value<string>("price")
                        ?.Replace(".", ",")!);
                    
                    if (currentPrice < alert.Price && alert.CheckDown || currentPrice > alert.Price && !alert.CheckDown)
                    {
                        await client.GetUser(alert.Id)
                            .SendMessageAsync(
                                $"{alert.Currency} just passed you alert price and now is at {currentPrice}");
                        
                        Globals.AlertList.Remove(alert);
                        if (Globals.AlertList.Count == 0) return;
                    }
                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}