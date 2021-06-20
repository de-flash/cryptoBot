using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using CryptoBot.ControlAPI;
using Discord;
using Discord.Commands;
using Newtonsoft.Json.Linq;

namespace CryptoBot.BotControl.Commands
{
    public class Change : ModuleBase<SocketCommandContext>
    {
        [Command("change")]
        public async Task ChangeAsync(string cryptoId, string interval)
        {
            var timeIntervals = new List<string> {"1h", "1d", "7d", "30d", "365d", "ytd"};
            await Context.Message.DeleteAsync();
            if (!new List<string> {"1h", "1d", "7d", "30d", "365d", "ytd"}.Contains(interval))
            {
                var reply = await ReplyAsync
                    ("Invalid time interval, check out `!help` to see valid ones.");
                await Task.Delay(3000);
                await reply.DeleteAsync();
                return;
            }
            var data = await MakeApiCall.GetTickerData(cryptoId, interval: interval);
            if (data.Count == 0)
            {
                var reply = await ReplyAsync
                    ("Could not find cryptocurrency you wanted, probably wrong input");
                await Task.Delay(3000);
                await reply.DeleteAsync();
                return;
            }
            var change = data.First!.Value<JObject>(interval);
            var embed = new EmbedBuilder
            {
                Title = $"Changes of {cryptoId.ToUpper()} in last {interval}",
                Timestamp = DateTimeOffset.Now,
                Color = Color.DarkBlue
            };
            embed.AddField("Price change", change.Value<string>("price_change") + " USD");
            embed.AddField("Percent price change",
                double.Parse(change.Value<string>("price_change_pct")!, CultureInfo.InvariantCulture) * 100 + "%");
            embed.AddField("Volume change", change.Value<string>("volume_change") + " USD");
            embed.AddField("Percent volume change",
                double.Parse(change.Value<string>("volume_change_pct")!, CultureInfo.InvariantCulture) * 100 + "%");

            await ReplyAsync(embed: embed.Build());
        }
    }
}