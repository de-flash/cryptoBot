using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoBot.Helper;
using Discord.Commands;

namespace CryptoBot.BotControl.Commands
{
    public class RemoveAlert : ModuleBase<SocketCommandContext>
    {
        [Command("removealert")]
        public async Task RemoveAlertAsync(string cryptoId, string stringPrice)
        {
            await Context.Message.DeleteAsync();
            
            if (!double.TryParse(stringPrice.Replace(".", ","), out var price))
            {
                var reply1 = await ReplyAsync("Price parameter is invalid");
                await Task.Delay(3000);
                await reply1.DeleteAsync();
                return;
            }
            
            var alertToRemove = Globals.AlertList
                .FirstOrDefault(alert => alert.Id == Context.User.Id && alert.Currency == cryptoId.ToUpper() &&
                                Math.Abs(alert.Price - price) < 0.001);
            if (alertToRemove is null)
            {
                var msg = await ReplyAsync("You can not remove non-existent alert, check `!myalerts` to see your alerts");
                await Task.Delay(3000);
                await msg.DeleteAsync();
                return;
            }
            Globals.AlertList.Remove(alertToRemove);
            var reply = await ReplyAsync("Alert has been successfully removed");
            await Task.Delay(3000);
            await reply.DeleteAsync();

        }
    }
}