using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;

namespace LegionBot
{
    class Program
    {

        private CommandService _commands;
        private DiscordSocketClient _client;
        private IServiceProvider _services;
        private IServiceCollection _serviceCollection;
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 100
            });
            _commands = new CommandService();
            _serviceCollection = new ServiceCollection()
                .AddSingleton(_commands)
                .AddSingleton(_client);
            string botToken = ConfigurationManager.AppSettings["botToken"];

            _client.Log += Log;

            _client.MessageReceived += HandleCommandAsync;

            await RegisterCommandsAsync();

            _services = _serviceCollection.BuildServiceProvider();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();

            await Task.Delay(-1);

        }

        private async Task RegisterCommandsAsync()
        {
            Assembly assembly = Assembly.LoadFrom(ConfigurationManager.AppSettings["TestModule"]);
            await _commands.AddModulesAsync(assembly, _services);
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("!", ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                await _commands.ExecuteAsync(context, argPos, _services);
            }

        }

        private Task Log(LogMessage logMessage)
        {
            Console.WriteLine(logMessage);
            return Task.CompletedTask;
        }
    }
}
