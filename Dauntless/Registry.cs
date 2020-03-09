using Discord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Dauntless
{
    static class Registry
    {
        public static List<Behemoth> Behemoths { get; set; }
        public static List<Weapon> Weapons { get; set; }
        public static Dictionary<Element, Color> ElementalColours { get; set; } = new Dictionary<Element, Color>();
        public static List<ArmourSet> ArmourSets { get; set; }

        public static Task Initialise()
        {
            LoadWeapons();
            LoadMonsters();
            LoadArmours();

            FileSystemWatcher weaponWatcher = new FileSystemWatcher();
            weaponWatcher.Path = ".";
            weaponWatcher.Filter = "Weapons.json";
            weaponWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
            | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            weaponWatcher.Changed += new FileSystemEventHandler(onWeaponJSONChanged);
            weaponWatcher.EnableRaisingEvents = true;

            FileSystemWatcher BehemothWatcher = new FileSystemWatcher();
            BehemothWatcher.Path = ".";
            BehemothWatcher.Filter = "dauntless.json";
            BehemothWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
            | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            BehemothWatcher.Changed += new FileSystemEventHandler(onBehemothJSONChanged);
            BehemothWatcher.EnableRaisingEvents = true;


            FileSystemWatcher armourWatcher = new FileSystemWatcher();
            armourWatcher.Path = ".";
            armourWatcher.Filter = "Armour.json";
            armourWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
            | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            armourWatcher.Changed += new FileSystemEventHandler(onArmourJSONChanged);
            armourWatcher.EnableRaisingEvents = true;

            return Task.CompletedTask;
        }

        public static Task LoadWeapons()
        {
            using (System.IO.StreamReader r = new System.IO.StreamReader("Weapons.json"))
            {
                string json = r.ReadToEnd();
                Weapons = JsonConvert.DeserializeObject<List<Weapon>>(json);
            }
            return Task.CompletedTask;
        }

        public static Task LoadMonsters()
        {
            using (System.IO.StreamReader r = new System.IO.StreamReader("dauntless.json"))
            {
                string json = r.ReadToEnd();
                Behemoths = JsonConvert.DeserializeObject<List<Behemoth>>(json);
            }
            ElementalColours.Add(Element.Blaze, Color.DarkRed);
            ElementalColours.Add(Element.Frost, Color.DarkBlue);
            ElementalColours.Add(Element.Neutral, Color.LightGrey);
            ElementalColours.Add(Element.Radiant, Color.DarkOrange);
            ElementalColours.Add(Element.Shock, Color.Gold);
            ElementalColours.Add(Element.Umbral, Color.DarkMagenta);
            return Task.CompletedTask;
        }

        public static Task LoadArmours()
        {
            using (System.IO.StreamReader r = new System.IO.StreamReader("Armour.json"))
            {
                string json = r.ReadToEnd();
                ArmourSets = JsonConvert.DeserializeObject<List<ArmourSet>>(json);
            }
            foreach (ArmourSet armour in ArmourSets)
            {
                armour.ValidateResistances();
            }
            return Task.CompletedTask;
        }

        private static void onWeaponJSONChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Updating weapon registry as the json file has changed");
            LoadWeapons();
        }

        private static void onBehemothJSONChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Updating behemoth registry as the json file has changed");
            LoadMonsters();
        }

        private static void onArmourJSONChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Updating armour registry as the json file has changed");
            LoadArmours();
        }
    }
}
