using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoBot.ControlAPI;
using CryptoBot.Helper;
using Discord.Commands;

namespace CryptoBot.BotControl.Commands
{
    public class SetAlert : ModuleBase<SocketCommandContext>
    {
        [Command("alert")]
        public async Task SetAlertAsync(string cryptoId, string stringPrice)
        {
            await Context.Message.DeleteAsync();
            if (!double.TryParse(stringPrice.Replace(".", ","), out var price))
            {
                var reply = await ReplyAsync("Price parameter is invalid");
                await Task.Delay(3000);
                await reply.DeleteAsync();
                return;
            }

            if (Globals.AlertList.Any(alert =>
                alert.Id == Context.User.Id && alert.Currency == cryptoId.ToUpper() && Math.Abs(alert.Price - price) < 0.0001))
            {
                var reply = await ReplyAsync
                    ("You already have the same alert set");
                await Task.Delay(3000);
                await reply.DeleteAsync();
                return;
            }
            
            var data = await MakeApiCall.GetTickerData(cryptoId);
            if (data.Count == 0)
            {
                var reply = await ReplyAsync
                    ("Could not find cryptocurrency you wanted, probably wrong input");
                await Task.Delay(3000);
                await reply.DeleteAsync();
                return;
            }
            var currentPrice = double.Parse(data
                .First(token => token.Value<string>("id") == cryptoId.ToUpper()).Value<string>("price")
                ?.Replace(".", ",")!);
            
            Globals.AlertList.Add(new Alert
            {
                Id = Context.Message.Author.Id,
                Currency = cryptoId.ToUpper(),
                Price = price,
                CheckDown = currentPrice > price
            });
            
            if (!Globals.CurrencyIds.Contains(cryptoId))
            {
                Globals.CurrencyIds += cryptoId + ",";
            }
            
            var msg = await Context.Channel.
                   SendMessageAsync($"When the price of {cryptoId.ToUpper()} is at {price} I will DM you");
            await Task.Delay(3000);
            await msg.DeleteAsync();
            
            
        }
    }
}