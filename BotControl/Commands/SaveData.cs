using System.IO;
using System.Threading.Tasks;
using CryptoBot.Helper;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace CryptoBot.BotControl.Commands
{
    public class SaveData : ModuleBase<SocketCommandContext>
    {
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("save")]
        public async Task SaveAsync()
        {
            await Context.Message.DeleteAsync();
            var jsonString = JsonConvert.SerializeObject(Globals.AlertList, Formatting.Indented);
            await File.WriteAllTextAsync("alerts.json", jsonString);
            await File.WriteAllTextAsync("currencyIDs.txt", Globals.CurrencyIds);

        }
    }
}