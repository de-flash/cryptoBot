using System.Threading.Tasks;
using CryptoBot.ControlAPI;
using Discord.Commands;

namespace CryptoBot.BotControl.Commands
{
    public class Price : ModuleBase<SocketCommandContext>
    {
        [Command("price")]
        public async Task GetPrice(string id = "ETH", string currency = "USD")
        {
            await Context.Message.DeleteAsync();
            var data = await MakeApiCall.GetTickerData(id, currency);
            if (data.Count == 0)
            {
                var reply = await ReplyAsync
                    ("Could not find cryptocurrency you wanted, probably wrong input");
                await Task.Delay(3000);
                await reply.DeleteAsync();
                return;
            }
            
            var price = data
                .First!.Value<string>("price");
            await Context.Channel
                .SendMessageAsync($"Current price of {id.ToUpper()} is {price} {currency.ToUpper()}");
        }
    }
}