using System;
using System.Collections.Generic;
using System.Text;

namespace Dauntless
{
    class Part
    {
        public string Location { get; set; }
        public int Count { get; set; }
    }
    class BreakPart
    {
        public string Name { get; set; }
        public List<string> Variants { get; set; }
        public List<Part> Parts { get; set; }
        public string Rarity { get; set; } = "Not listed";
    }
}
