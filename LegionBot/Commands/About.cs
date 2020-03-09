using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LegionBot.Commands
{
    public class About : ModuleBase<SocketCommandContext>
    {
        [Command("Author")]
        public async Task Author()
        {
            EmbedBuilder embed = new EmbedBuilder()
                .WithTitle("Conor Wood")
                .WithDescription("I like to develop things.")
                .WithUrl("https://github.com/gamedevraccoon")
                .WithColor(Color.Teal);

            await ReplyAsync("", false, embed.Build());
        }
    }
}
