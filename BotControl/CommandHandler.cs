using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace CryptoBot.BotControl
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;

        // Retrieve client and CommandService instance via constructor
        public CommandHandler(DiscordSocketClient client, CommandService commandService)
        {
            this._client = client;
            this._commandService = commandService;
        }

        public async Task SetupAsync()
        {
            // Hook the MessageReceived event into our command handler
            _client.MessageReceived += HandleCommandAsync;

            // Here we discover all of the command modules in the entry assembly and load them
            await _commandService.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            // Create a number to track where the prefix ends and the command begins
            var argPos = 0;
            
            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!message.HasCharPrefix('!', ref argPos) ||
                message.Author.IsBot)
                return;

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_client, message);

            // Execute the command with the command context we just created
            var result = await _commandService.ExecuteAsync(context: context, argPos: argPos, services: null);
            
            if (!result.IsSuccess)
            {
                await context.Channel.DeleteMessageAsync(message);
                var msg = await context.Channel.SendMessageAsync("Wrong input, try again or check `!help`");
                await Task.Delay(5000);
                await context.Channel.DeleteMessageAsync(msg);
            }
        }
    }
}