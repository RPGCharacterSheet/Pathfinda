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
    }
}
