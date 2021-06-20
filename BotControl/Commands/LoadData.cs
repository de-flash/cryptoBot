using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CryptoBot.Helper;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace CryptoBot.BotControl.Commands
{
    public class LoadData : ModuleBase<SocketCommandContext>
    {
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("load")]
        public async Task LoadAsync()
        {
            await Context.Message.DeleteAsync();
            Globals.AlertList = JsonConvert.DeserializeObject<List<Alert>>(await File.ReadAllTextAsync("alerts.json"));
            Globals.CurrencyIds = await File.ReadAllTextAsync("currencyIDs.txt");
        }
    }
}