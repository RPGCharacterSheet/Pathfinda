using MongoModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pathfinda
{
    public static class ExtensionMethods
    {
        public static int Modifier(this AbilityScore a)
        {
                // 1:       -5
                // 2-3:     -4
                // 10-11:   0
                // 18-19:   +4
                return (int)((double)a.Score / 2d) - 5;
        }

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

        public static int Speed(this Races r)
        {
            switch (r)
            {
                case Races.Dwarf:
                case Races.Gnome:
                case Races.Halfling:
                case Races.Oread:
                case Races.Ratfolk:
                    return 20;
                default:
                case Races.Elf:
                case Races.HalfElf:
                case Races.HalfOrc:
                case Races.Human:
                case Races.Aasimar:
                case Races.Catfolk:
                case Races.Dhampir:
                case Races.Drow:
                case Races.Fetchling:
                case Races.Goblin:
                case Races.Hobgoblin:
                case Races.Ifrit:
                case Races.Kobold:
                case Races.Orc:
                case Races.Sylph:
                case Races.Tengu:
                case Races.Tiefling:
                case Races.Undine:
                    return 30;
            }
        }

        public static List<ItemProperties> SkillBonuses(this Races r)
        {
            switch (r)
            {
                case Races.Dwarf:
                    return new List<ItemProperties>() { ItemProperties.Appraise, ItemProperties.Perception };
                case Races.Elf:
                    return new List<ItemProperties>() { ItemProperties.Perception };
                case Races.Gnome: // gnome also gets to choose from Craft or Profession. The user will have to add a Character modifier manually
                    return new List<ItemProperties>() { ItemProperties.Perception };
                case Races.HalfElf:
                    return new List<ItemProperties>() { ItemProperties.Perception };
                case Races.HalfOrc:
                    return new List<ItemProperties>() { ItemProperties.Intimidate };
                case Races.Halfling:
                    return new List<ItemProperties>() { ItemProperties.Perception, ItemProperties.Acrobatics, ItemProperties.Climb };
                default:
                case Races.Human:
                    return new List<ItemProperties>();
                case Races.Aasimar:
                    return new List<ItemProperties>() { ItemProperties.Diplomacy, ItemProperties.Perception };
                case Races.Catfolk:
                    return new List<ItemProperties>() { ItemProperties.Perception, ItemProperties.Stealth, ItemProperties.Survival };
                case Races.Dhampir:
                    return new List<ItemProperties>() { ItemProperties.Bluff, ItemProperties.Perception };
                case Races.Drow:
                    return new List<ItemProperties>() { ItemProperties.Perception };
                case Races.Fetchling:
                    return new List<ItemProperties>() { ItemProperties.KnowledgePlanes, ItemProperties.Stealth };
                case Races.Goblin:
                    return new List<ItemProperties>() { ItemProperties.Ride, ItemProperties.Stealth };
                case Races.Hobgoblin:
                    return new List<ItemProperties>() { ItemProperties.Stealth };
                case Races.Ifrit:
                    return new List<ItemProperties>();
                case Races.Kobold: // specifically Craft (trapmaking) and Profession (miner)
                    return new List<ItemProperties>() { ItemProperties.Craft, ItemProperties.Perception, ItemProperties.Profession };
                case Races.Orc:
                    return new List<ItemProperties>();
                case Races.Oread:
                    return new List<ItemProperties>();
                case Races.Ratfolk: // specifically Craft(alchemy) and the Handle Animal bonus is +4
                    return new List<ItemProperties>() { ItemProperties.Craft, ItemProperties.HandleAnimal, ItemProperties.Perception, ItemProperties.UseMagicDevice };
                case Races.Sylph:
                    return new List<ItemProperties>();
                case Races.Tengu: // Linguistics +4
                    return new List<ItemProperties>() { ItemProperties.Linguistics, ItemProperties.Perception, ItemProperties.Stealth };
                case Races.Tiefling:
                    return new List<ItemProperties>() { ItemProperties.Bluff, ItemProperties.Stealth };
                case Races.Undine:
                    return new List<ItemProperties>();
            }
        }
    }
}
