using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace CryptoBot.BotControl.Commands
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HelpAsync()
        {
            await Context.Message.DeleteAsync();
            var embed = new EmbedBuilder
            {
                Title = "Hey, I hope this helps",
                Description = "You can use these commands",
                Color = Color.DarkBlue,
                Timestamp = DateTimeOffset.Now,
            };
            embed.AddField("!price `cryptoID` `currency`",
                "Replies with current price of `cryptoID` in `currency`");
            embed.AddField("!alert `cryptoID` `priceToAwait`",
                "Sets an alert based on your input, when the price crosses the price you set, bot will send" +
                "you DM");
            embed.AddField("!myalerts", "Bots responds with all your set alerts");
            embed.AddField("!removealert `cryptoID` `price`", "Based on your input bot removes one of your alerts");
            embed.AddField("!removeall", "Removes all you alerts");
            embed.AddField("!coininfo `cryptoID`",
                "Bot responds with basic information about the cryptocurrency you requested");
            embed.AddField("!metadata `cryptoId`", "Find all the links to cryptocurrency you requested.");
            embed.AddField("!change `cryptoID` `timeInterval`",
                "Shows price change in chosen interval \n Possible intervals: `1h`, `1d`, `7d`, `30d`, `365d`, `ytd`");

            await ReplyAsync(embed: embed.Build());
        }
    }
}