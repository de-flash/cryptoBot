using System;
using System.Threading.Tasks;
using CryptoBot.ControlAPI;
using Discord;
using Discord.Commands;

namespace CryptoBot.BotControl.Commands
{
    public class CoinInfo : ModuleBase<SocketCommandContext>

    {
        [Command("coininfo")]
        public async Task CoinInfoAsync(string cryptoId)
        {
            await Context.Message.DeleteAsync();
            var data = await MakeApiCall.GetTickerData(cryptoId);
            if (data.Count == 0)
            {
                var reply = await ReplyAsync
                    ("Could not find cryptocurrency you wanted, probably wrong input");
                await Task.Delay(3000);
                await reply.DeleteAsync();
                return;
            }
            
            var cryptoData = data.First;
            
            var embed = new EmbedBuilder
            {
                Timestamp = DateTimeOffset.Now,
                Title = "Coin information",
                Color = Color.DarkBlue,
            };
            embed.AddField("Currency:", cryptoData.Value<string>("currency"), true);
            embed.AddField("Name", cryptoData.Value<string>("name"), true);
            embed.AddField("Price", cryptoData.Value<string>("price") + " USD", true);
            embed.AddField("Circulating supply", cryptoData.Value<string>("circulating_supply") + " USD", true);
            embed.AddField("Market cap dominance", cryptoData.Value<string>("market_cap_dominance") + "%", true);
            embed.AddField("Exchanges in 1 hour", cryptoData.Value<string>("num_exchanges"), true);
            embed.AddField("All time high", cryptoData.Value<string>("high") + " USD", true);
            

            
            await ReplyAsync(embed: embed.Build());
        }
        
    }
}