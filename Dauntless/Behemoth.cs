using System;
using System.Collections.Generic;
using System.Text;

namespace Dauntless
{
    enum Element { Neutral, Blaze, Frost, Shock, Radiant, Umbral}
    class Behemoth
    {
        public string Name { get; set; }
        public List<Element> Weaknesses { get; set; }
        public Element Element { get; set; }
        public string Location { get; set; }
        public List<BreakPart> BreakParts { get; set; }
        public List<string> SharedDrops { get; set; }
    }
}
