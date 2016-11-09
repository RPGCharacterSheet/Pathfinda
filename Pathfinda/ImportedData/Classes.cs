using MongoModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinda
{
    public static partial class ImportedData
    {
        // templates generated from Pathfinder Autosheet Google Sheet, Spellcasting tab at the bottom.

        // use this for classes that don't cast spells
        private static int[,] NoSpellsTemplate = new int[21, 10];

        // use this for sorcerer casts/day
        private static int[,] SpontaneousCasterTemplate = new int[21, 10]
        {
            //  0   1   2   3   4   5   6   7   8   9
            {   0,  0,  0,  0,  0,  0,  0,  0,  0,  0}, // level 0 (placeholder only)
            {   100,3,  0,  0,  0,  0,  0,  0,  0,  0}, // level 1
            {   100,4,  0,  0,  0,  0,  0,  0,  0,  0},
            {   100,5,  0,  0,  0,  0,  0,  0,  0,  0},
            {   100,6,  3,  0,  0,  0,  0,  0,  0,  0},
            {   100,6,  4,  0,  0,  0,  0,  0,  0,  0}, // level 5
            {   100,6,  5,  3,  0,  0,  0,  0,  0,  0},
            {   100,6,  6,  4,  0,  0,  0,  0,  0,  0},
            {   100,6,  6,  5,  3,  0,  0,  0,  0,  0},
            {   100,6,  6,  6,  4,  0,  0,  0,  0,  0},
            {   100,6,  6,  6,  5,  3,  0,  0,  0,  0}, // level 10
            {   100,6,  6,  6,  6,  4,  0,  0,  0,  0},
            {   100,6,  6,  6,  6,  5,  3,  0,  0,  0},
            {   100,6,  6,  6,  6,  6,  4,  0,  0,  0},
            {   100,6,  6,  6,  6,  6,  5,  3,  0,  0},
            {   100,6,  6,  6,  6,  6,  6,  4,  0,  0}, // level 15
            {   100,6,  6,  6,  6,  6,  6,  5,  3,  0},
            {   100,6,  6,  6,  6,  6,  6,  6,  4,  0},
            {   100,6,  6,  6,  6,  6,  6,  6,  5,  3},
            {   100,6,  6,  6,  6,  6,  6,  6,  6,  4},
            {   100,6,  6,  6,  6,  6,  6,  6,  6,  6}, // level 20
        };

        // use this for sorcerer spells known
        private static int[,] SpontaneousLearnerTemplate = new int[21, 10]
        {
            //  0   1   2   3   4   5   6   7   8   9
            {   0,  0,  0,  0,  0,  0,  0,  0,  0,  0}, // level 0 (placeholder only)
            {   4,  2,  0,  0,  0,  0,  0,  0,  0,  0}, // level 1
            {   5,  2,  0,  0,  0,  0,  0,  0,  0,  0},
            {   5,  3,  0,  0,  0,  0,  0,  0,  0,  0},
            {   6,  3,  1,  0,  0,  0,  0,  0,  0,  0},
            {   6,  4,  2,  0,  0,  0,  0,  0,  0,  0}, // level 5
            {   7,  4,  2,  1,  0,  0,  0,  0,  0,  0},
            {   7,  5,  3,  2,  0,  0,  0,  0,  0,  0},
            {   8,  5,  3,  2,  1,  0,  0,  0,  0,  0},
            {   8,  5,  4,  3,  2,  0,  0,  0,  0,  0},
            {   9,  5,  4,  3,  2,  1,  0,  0,  0,  0}, // level 10
            {   9,  5,  5,  4,  3,  2,  0,  0,  0,  0},
            {   9,  5,  5,  4,  3,  2,  1,  0,  0,  0},
            {   9,  5,  5,  4,  4,  3,  2,  0,  0,  0},
            {   9,  5,  5,  4,  4,  3,  2,  1,  0,  0},
            {   9,  5,  5,  4,  4,  4,  3,  2,  0,  0}, // level 15
            {   9,  5,  5,  4,  4,  4,  3,  2,  1,  0},
            {   9,  5,  5,  4,  4,  4,  3,  3,  2,  0},
            {   9,  5,  5,  4,  4,  4,  3,  3,  2,  1},
            {   9,  5,  5,  4,  4,  4,  3,  3,  3,  2},
            {   9,  5,  5,  4,  4,  4,  3,  3,  3,  3}, // level 20
        };

        // use this for wizard casts/day and spells known
        private static int[,] PreparedCasterTemplate = new int[21, 10]
        {
            //  0   1   2   3   4   5   6   7   8   9
            {   0,  0,  0,  0,  0,  0,  0,  0,  0,  0}, // level 0 (placeholder only)
            {   3,  1,  0,  0,  0,  0,  0,  0,  0,  0}, // level 1
            {   4,  2,  0,  0,  0,  0,  0,  0,  0,  0},
            {   4,  2,  1,  0,  0,  0,  0,  0,  0,  0},
            {   4,  3,  2,  0,  0,  0,  0,  0,  0,  0},
            {   4,  3,  2,  1,  0,  0,  0,  0,  0,  0}, // level 5
            {   4,  3,  3,  2,  0,  0,  0,  0,  0,  0},
            {   4,  4,  3,  2,  1,  0,  0,  0,  0,  0},
            {   4,  4,  3,  3,  2,  0,  0,  0,  0,  0},
            {   4,  4,  4,  3,  2,  1,  0,  0,  0,  0},
            {   4,  4,  4,  3,  3,  2,  0,  0,  0,  0}, // level 10
            {   4,  4,  4,  4,  3,  2,  1,  0,  0,  0},
            {   4,  4,  4,  4,  3,  3,  2,  0,  0,  0},
            {   4,  4,  4,  4,  4,  3,  2,  1,  0,  0},
            {   4,  4,  4,  4,  4,  3,  3,  2,  0,  0},
            {   4,  4,  4,  4,  4,  4,  3,  2,  1,  0}, // level 15
            {   4,  4,  4,  4,  4,  4,  3,  3,  2,  0},
            {   4,  4,  4,  4,  4,  4,  4,  3,  2,  1},
            {   4,  4,  4,  4,  4,  4,  4,  3,  3,  2},
            {   4,  4,  4,  4,  4,  4,  4,  4,  3,  3},
            {   4,  4,  4,  4,  4,  4,  4,  4,  4,  4}, // level 20
        };

        public class Class
        {
            public string Name { get; set; }
            public int HitDice { get; set; }
            public double BABGrowth { get; set; }
            public double FortGrowth { get; set; }
            public double ReflexGrowth { get; set; }
            public double WillGrowth { get; set; }
            public int SkillGrowth { get; set; }
            public List<ItemProperties> ClassSkills { get; set; }
            public Abilities? SpellcastingBonus { get; set; }
        }

        private static List<Class> _classes = null;
        public static List<Class> Classes
        {
            get
            {
                if (_classes == null)
                {
                    _classes = new List<Class>()
                    {

                    };
                }
                return _classes;
            }
        }
    }
}
