using System.Collections.Generic;

namespace Dauntless
{
    class Weapon
    {
        public string Name { get; set; }
        public int PhysicalDamage { get; set; }
        public ElementalAffinity ElementalPower {get; set;}
        public List<CellSlot> CellSlots { get; set; }
        public string UniqueEffect { get; set; }
    }
}