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

namespace LegionBot
{
    class Program
    {

        private CommandService _commands;
        private DiscordSocketClient _client;
        private IServiceProvider _services;
        private IServiceCollection _serviceCollection;
        private Configuration _configuration;
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 100
            });
            using (System.IO.StreamReader r = new System.IO.StreamReader("config.json"))
            {
                string json = r.ReadToEnd();
                _configuration = JsonConvert.DeserializeObject<Configuration>(json);
            }
            _commands = new CommandService();
            _serviceCollection = new ServiceCollection()
                .AddSingleton(_commands)
                .AddSingleton(_client);
            string botToken = _configuration.Token;

            _client.Log += Log;

            FileSystemWatcher librarywatcher = new FileSystemWatcher();
            librarywatcher.Path = ".";
            librarywatcher.Filter = "*Commands.dll";
            librarywatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
            | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            librarywatcher.Deleted += new FileSystemEventHandler(onLibraryDeleted);
            librarywatcher.Created += new FileSystemEventHandler(onLibraryAdded);

            librarywatcher.EnableRaisingEvents = true;

            _client.MessageReceived += HandleCommandAsync;

            await RegisterCommandsAsync();

            _services = _serviceCollection.BuildServiceProvider();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();

            await Task.Delay(-1);

        }

        private void onLibraryAdded(object sender, FileSystemEventArgs e)
        {
            Assembly assembly = Assembly.LoadFrom(e.FullPath);
            string typeName = e.Name.Remove(e.Name.LastIndexOf('.'));
            typeName = $"{typeName}.{typeName}";
            Type type = assembly.GetType(typeName);

            var methodInfo = type.GetMethod("Initialise");
            if (methodInfo == null) // the method doesn't exist
            {
                // throw some exception
            }
            var o = Activator.CreateInstance(type);
            Console.WriteLine($"Reloaded {e.Name}");
            var result = methodInfo?.Invoke(o, null);
            _commands.AddModulesAsync(assembly);
        }

        private void onLibraryDeleted(object sender, FileSystemEventArgs e)
        {
            foreach (ModuleInfo pluginInfo in _commands.Modules)
            {
                if (pluginInfo.Name == e.Name) _commands.RemoveModuleAsync(pluginInfo);
            }
        }

        private async Task RegisterCommandsAsync()
        {

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());

            foreach (string file in Directory.EnumerateFiles(".", "*Commands.dll", SearchOption.AllDirectories))
            {
                if (file.Contains("Discord.Net.Commands")) continue;
                Assembly assembly = Assembly.LoadFrom(file);
                string typeName = file.Remove(file.LastIndexOf('.'));
                typeName = typeName.Substring(2);
                typeName = $"{typeName}.{typeName}";
                Type type = assembly.GetType(typeName);

                var methodInfo = type.GetMethod("Initialise");
                if (methodInfo == null) // the method doesn't exist
                {
                    // throw some exception
                }
                var o = Activator.CreateInstance(type);

                var result = methodInfo?.Invoke(o, null);

                await _commands.AddModulesAsync(assembly);
            }
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
