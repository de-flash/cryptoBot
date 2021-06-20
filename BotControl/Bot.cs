using System;
using System.IO;
using System.Threading.Tasks;
using CryptoBot.ControlAPI;
using CryptoBot.Helper;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace CryptoBot.BotControl
{
    public class Bot
    {
        public static async Task MainAsync()
        {
            var client = new DiscordSocketClient();
            var commandService = new CommandService();

            // Log information to the console
            client.Log += Log;

            // Read the token for your bot from file
            var token = await File.ReadAllTextAsync
                (@"C:\Users\majus\RiderProjects\CryptoBot\CryptoBot\BotControl\token.txt");

            // Log in to Discord
            await client.LoginAsync(TokenType.Bot, token);

            // Start connection logic
            await client.StartAsync();

            // Create files for !data and !save commands
            if (!File.Exists("alerts.json")) File.Create("alerts.json");
            if (!File.Exists("currencyIDs.txt")) File.Create("currencyIDs.txt");
            
            // Update data every minute
            var timer = new System.Timers.Timer(60000);
            timer.Elapsed += async ( sender, e ) => await Ticker
                .CheckAlerts(client, Globals.CurrencyIds);
            timer.Start();

            // Here you can set up your event handlers
            await new CommandHandler(client, commandService).SetupAsync();

            // Block this task until the program is closed
            await Task.Delay(-1);
        }

        private static Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
    }
}