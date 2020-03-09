using System;
using System.Collections.Generic;
using System.Text;

namespace Dauntless
{
    public enum CellSlot { Defence, Mobility, Power, Technique, Utility}
    class ElementalAffinity
    {
        public Element Affinity { get; set; }
        public int Amount { get; set; }
    }
    class ArmourSet
    {
        public string Name { get; set; }
        public Armour HeadGear { get; set; }
        public Armour ChestPiece { get; set; }
        public Armour Gloves { get; set; }
        public Armour Shoes { get; set; }
        public ElementalAffinity ElementalResistance { get; set; }

        internal void ValidateResistances()
        {
            ElementalResistance = new ElementalAffinity
            {
                Affinity = HeadGear.ElementalResistance.Affinity,
                Amount = (HeadGear.ElementalResistance.Amount + ChestPiece.ElementalResistance.Amount
                            + Gloves.ElementalResistance.Amount + Shoes.ElementalResistance.Amount)
            };
        }
    }
    class Armour
    {
        public string Name { get; set; }
        public CellSlot CellSlot { get; set; }
        public int PhysicalResistance { get; set; }
        public ElementalAffinity ElementalResistance { get; set; }
        public ElementalAffinity ElementalWeakness { get; set; }
        public List<string> Perks { get; set; }
    }

}
