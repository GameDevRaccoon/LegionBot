using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace TestModule
{
    public class TestCommand : ModuleBase<SocketCommandContext>
    {
        [Command("Test")]
        public async Task Test()
        {
            EmbedBuilder embed = new EmbedBuilder()
                .WithTitle("Test Command")
                .WithDescription("This command was loaded from another module.")
                .WithColor(Color.Green);

            await ReplyAsync("", false, embed.Build());
        }
    }
}
