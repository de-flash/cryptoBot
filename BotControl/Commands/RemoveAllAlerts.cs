using System.Linq;
using System.Threading.Tasks;
using CryptoBot.Helper;
using Discord.Commands;

namespace CryptoBot.BotControl.Commands
{
    public class RemoveAllAlerts : ModuleBase<SocketCommandContext>

    {
        [Command("removeall")]
        public async Task RemoveAllAlertsAsync()
        {
            await Context.Message.DeleteAsync();
            if (Globals.AlertList.All(alert => alert.Id != Context.User.Id))
            {
                var reply = await ReplyAsync("You dont have any alerts set");
                await Task.Delay(3000);
                await reply.DeleteAsync();
                return;
            }
            Globals.AlertList.RemoveAll(alert => alert.Id == Context.User.Id);
            var success = await ReplyAsync("All you alerts have been removed.");
            await Task.Delay(3000);
            await success.DeleteAsync();
        }
    }
}