using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoBot.Helper;
using Discord;
using Discord.Commands;

namespace CryptoBot.BotControl.Commands
{
    public class CurrentAlerts : ModuleBase<SocketCommandContext>
    {
        [Command("myalerts")]
        public async Task MyAlertsAsync()
        {
            await Context.Message.DeleteAsync();
            var myAlerts = Globals.AlertList
                .Where(alert => alert.Id == Context.User.Id)
                .ToList();
            if (myAlerts.Count == 0)
            {
                var msg = await ReplyAsync("You do not have any alerts set.");
                await Task.Delay(3000);
                await msg.DeleteAsync();
                return;
            }
            var embed = new EmbedBuilder
            {
                Title = "These are the alerts you set.",
                Color = Color.DarkBlue,
                Timestamp = DateTimeOffset.Now,
            };
            foreach (var alert in myAlerts)
            {
                embed.AddField("Cryptocurrency: " + alert.Currency, $"Threshold at: {alert.Price} USD");
            }

            await Context.User.SendMessageAsync(embed: embed.Build());
        }
    }
}