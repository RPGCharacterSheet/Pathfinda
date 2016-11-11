using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoModels.Models
{
    public class Class : MongoEntityBase<Class>
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public int HitDice { get; set; }
        public double BABGrowth { get; set; }
        public SaveGrowth FortGrowth { get; set; }
        public SaveGrowth ReflexGrowth { get; set; }
        public SaveGrowth WillGrowth { get; set; }
        public int SkillGrowth { get; set; }
        public List<ItemProperties> ClassSkills { get; set; }
        public Abilities? SpellCastingBonus { get; set; }
    }
}
