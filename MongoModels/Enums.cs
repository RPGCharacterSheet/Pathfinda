using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MongoModels
{


    public class MiscEffect
    {
        public ItemProperties Property { get; set; }
        public int Value { get; set; }
        public string Source { get; set; }
    }

    /// <summary>
    /// Could be armor, weapons, scrolls, rope, whatever
    /// </summary>
    public class Gear
    {
        public string Name { get; set; }
        public bool IsEquipped { get; set; } = false;
        /// <summary>
        /// In lbs.
        /// Remember: Armor fitted for Small characters weighs half as much, and armor fitted for Large characters weighs twice as much
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Would be false if the item is carried in a bag of holding or a saddlebag or otherwise does not count against your carrying capacity.
        /// </summary>
        public bool WeightCounts { get; set; } = true;
        /// <summary>
        /// In Gold pieces
        /// </summary>        
        public double Value { get; set; }
        /// <summary>
        /// For weapons only
        /// e.g. 1d4 
        /// or 2d8+2
        /// </summary>
        public string Damage { get; set; }
        /// <summary>
        /// For weapons only
        /// e.g. 19-20/x2 
        /// or x3
        /// </summary>
        public string Critical { get; set; }
        /// <summary>
        /// e.g. Nonlethal, disarm, breaks on critical fail
        /// </summary>
        public string Special { get; set; }

        public Dictionary<ItemProperties, int> Properties { get; set; }
        public List<ItemDescriptors> Descriptors { get; set; }

        public Gear()
        {
            Properties = new Dictionary<ItemProperties, int>();
            Descriptors = new List<ItemDescriptors>();
        }
    }

    /// <summary>
    /// http://paizo.com/pathfinderRPG/prd/ultimateCampaign/campaignSystems/alignment.html
    /// </summary>
    public enum Alignments { LawfulGood, LawfulNeutral, LawfulEvil, NeutralGood, TrueNeutral, NeutralEvil, ChaoticGood, ChaoticNeutral, ChaoticEvil }
    /// <summary>
    /// http://paizo.com/pathfinderRPG/prd/coreRulebook/gettingStarted.html#ability-scores
    /// </summary>
    public enum Abilities { Strength, Dexterity, Constitution, Wisdom, Intelligence, Charisma }
    /// <summary>
    /// http://www.d20srd.org/srd/combat/movementPositionAndDistance.htm#bigandLittleCreaturesInCombat
    /// </summary>
    public enum Sizes { Fine = 8, Diminuitive = 4, Tiny = 2, Small = 1, Medium = 0, Large = -1, Huge = -2, Gargantuan = -4, Colossal = -8 }
    /// <summary>
    /// Properties that items can have which require no extra data    
    /// </summary>
    public enum ItemDescriptors { Simple, Martial, Exotic, Blunt, Piercing, Slashing, Fragile, Broken }
    /// <summary>
    /// Properties which require a number to tell us how much the enhancement is (like Dexterity +2)
    /// http://paizo.com/pathfinderRPG/prd/ultimateEquipment/armsAndArmor/armor.html
    /// </summary>
    public enum ItemProperties
    {
        Strength,
        Dexterity,
        Constitution,
        Wisdom,
        Intelligence,
        Charisma,
        SpellFailure, // expressed as a whole number percentage
        SpellResistance,
        ArmorWeight, // matches Load enum: 0 for light, etc
        ArmorBonus,
        NaturalArmor,
        Deflection,
        Dodge,
        MaxDexBonus,
        ArmorCheckPenalty,
        ExtraSpeedPenalty, // beyond the normal penalty for Medium or Heavy armor
        ReachInFeet,
        FortitudeBonus,
        ReflexBonus,
        WillBonus,
        CombatManeuverDefense,
    }

    /// <summary>
    /// http://paizo.com/pathfinderRPG/prd/coreRulebook/races.html
    /// </summary>
    public enum Races { Dwarf, Elf, Gnome, HalfElf, HalfOrc, Halfling, Human, Aasimar, Catfolk, Dhampir, Drow, Fetchling, Goblin, Hobgoblin, Ifrit, Kobold, Orc, Oread, Ratfolk, Sylph, Tengu, Tiefling, Undine }

    /// <summary>
    /// http://www.d20pfsrd.com/alignment-description/carrying-capacity#TOC-Table-Carrying-Capacity
    /// </summary>
    public enum Loads
    {
        Light, // no bonus/penalty
        Medium, // Dexterity bonus is limited to 3. Check Penalty to all Str and Dex based skills: -3. Speed 30->20  20->15. Run x4
        Heavy, // Dexterity bonus is limited to 1. Chec Penalty to all Str and Dex based skills: -6 Speed 30->20  20->15.  Run x3
        Overloaded // No Dex bonus to AC. Movement is limited to 5 ft / round.
    }

    public static class ExtensionMethods
    {
        public static string ToSentence(this Alignments a)
        {
            return Regex.Replace(a.ToString(), "([A-Z])", " $1").Trim();
        }

        public static int ACAndAttackBonus(this Sizes s)
        {
            switch (s)
            {
                case Sizes.Fine:
                    return -8;
                case Sizes.Diminuitive:
                    return -4;
                case Sizes.Tiny:
                    return -2;
                case Sizes.Small:
                    return -1;
                default:
                case Sizes.Medium:
                    return 0;
                case Sizes.Large:
                    return 1;
                case Sizes.Huge:
                    return 2;
                case Sizes.Gargantuan:
                    return 4;
                case Sizes.Colossal:
                    return 8;
            }
        }

        public static int GrappleModifier(this Sizes s)
        {
            return -1 * s.HideModifier();
        }

        public static int HideModifier(this Sizes s)
        {
            switch (s)
            {
                case Sizes.Fine:
                    return 16;
                case Sizes.Diminuitive:
                    return 12;
                case Sizes.Tiny:
                    return 8;
                case Sizes.Small:
                    return 4;
                case Sizes.Medium:
                default:
                    return 0;
                case Sizes.Large:
                    return -4;
                case Sizes.Huge:
                    return -8;
                case Sizes.Gargantuan:
                    return -12;
                case Sizes.Colossal:
                    return -16;
            }
        }

        // penalty for str and dex based skills when you are encumbered
        public static int SkillCheckPenalty(this Loads l)
        {
            switch (l)
            {
                default:
                case Loads.Light:
                    return 0;
                case Loads.Medium:
                    return -3;
                case Loads.Heavy:
                case Loads.Overloaded:
                    return -6;
            }
        }

        // can't receive whole dex bonus in heavy armor
        public static int MaxDexBonus(this Loads l)
        {
            switch (l)
            {
                default:
                case Loads.Light:
                    return 100;
                case Loads.Medium:
                    return 3;
                case Loads.Heavy:
                    return 1;
                case Loads.Overloaded:
                    return 0;
            }
        }
    }
}
