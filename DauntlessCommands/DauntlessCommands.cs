using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DauntlessCommands
{
    [Group("Dauntless")]
    public class DauntlessCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Gear")]
        public async Task Gear([Remainder]string monster)
        {
            EmbedFieldBuilder armourField = new EmbedFieldBuilder { Name = "Armour", IsInline = true };
            EmbedFieldBuilder weaponField = new EmbedFieldBuilder { Name = "Weapon", IsInline = true };
            Behemoth localMonster = Registry.Behemoths?.Find(x => x.Name.ToLower() == monster.ToLower());
            if (localMonster != null)
            {
                string armourRetVal = $"{localMonster.Element}\n";
                string weaponRetVal = localMonster.Weaknesses.Count > 1 ? $"{localMonster.Weaknesses[0]} or {localMonster.Weaknesses[1]}\n" : $"{localMonster.Weaknesses[0]}\n";
                // Get any armours we know about that resist the behemoth
                List<ArmourSet> armours = Registry.ArmourSets?.FindAll(x => x.ElementalResistance.Affinity == localMonster.Element);
                foreach (ArmourSet armour in armours)
                {
                    armourRetVal += $"{armour.Name}: +{armour.ElementalResistance.Amount} {armour.ElementalResistance.Affinity} \n";
                }
                armourField.Value = armourRetVal;

                // Get any Weapons we know about that damage the behemoth
                List<Weapon> weapons = Registry.Weapons?.FindAll(x => localMonster.Weaknesses.Contains(x.ElementalPower.Affinity));
                foreach (Weapon weapon in weapons)
                {
                    weaponRetVal += $"{weapon.Name}: +{weapon.ElementalPower.Amount} {weapon.ElementalPower.Affinity}\n";
                }
                weaponField.Value = weaponRetVal;


                EmbedBuilder embed = new EmbedBuilder()
                    .AddField(armourField)
                    .AddField(weaponField);

                embed.Title = localMonster.Name;
                embed.Color = Registry.ElementalColours[localMonster.Element];
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                await ReplyAsync("Could not find a behemoth with that name");
            }
        }

        public async Task Initialise()
        {
            await Registry.Initialise();
            Console.WriteLine("Initialised Dauntless commands ");
            await Task.CompletedTask;
        }

        [Command("Drops")]
        public async Task Drops([Remainder]string behemoth)
        {
            EmbedBuilder embed = new EmbedBuilder();
            Behemoth localBehemoth = Registry.Behemoths?.Find(x => x.Name.ToLower() == behemoth.ToLower());
            foreach(BreakPart breakPart in localBehemoth.BreakParts)
            {
                EmbedFieldBuilder locationField = new EmbedFieldBuilder();
                EmbedFieldBuilder amountField = new EmbedFieldBuilder();
                locationField.Name = breakPart.Name;
                string locations = "";
                int count = 0;
                foreach(Part part in breakPart.Parts)
                {
                    locations += $"{part.Location} ";
                    count += part.Count;
                }
                locationField.Value = locations;
                locationField.IsInline = true;
                amountField.Name = "Amount";
                amountField.Value = count;
                amountField.IsInline = true;
                EmbedFieldBuilder rarityField = new EmbedFieldBuilder { Name = "Rarity", Value = breakPart.Rarity, IsInline = true };
                embed.AddField(locationField);
                embed.AddField(amountField);
                embed.AddField(rarityField);

            }

            await ReplyAsync("", false, embed.Build());
        }
    }
}
