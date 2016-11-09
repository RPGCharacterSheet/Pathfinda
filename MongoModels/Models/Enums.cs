using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoModels.Models
{
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

}
