using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoBot.ControlAPI;
using Discord;
using Discord.Commands;

namespace CryptoBot.BotControl.Commands
{
    public class MetaData : ModuleBase<SocketCommandContext>
    {
        [Command("metadata")]
        public async Task MetaDataAsync(string cryptoId)
        {
            await Context.Message.DeleteAsync();
            var data = await MakeApiCall.GetMetaData(cryptoId);
            if (data.Count == 0)
            {
                var reply = await ReplyAsync
                    ("Could not find cryptocurrency you wanted, probably wrong input");
                await Task.Delay(3000);
                await reply.DeleteAsync();
                return;
            }

            var embed = new EmbedBuilder
            {
                Title = "These are the links I could get you",
                Timestamp = DateTimeOffset.Now,
                Color = Color.DarkBlue
            };
            foreach (var token in data.First!.Skip(4).Take(16))
            {
                if (token.First!.ToString().Length != 0)
                    embed.AddField(token.ToString().Split(":")[0].Replace("\"", "").ToUpperInvariant(), token.First);
            }

            await ReplyAsync(embed: embed.Build());
        }
    }
}