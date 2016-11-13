using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
namespace MongoModels.Models
{    
    public class Character : MongoEntityBase
    {
        public ObjectId Owner { get; set; }
        public List<ObjectId> Shared { get; set; }
        public virtual string Name { get; set; }
        public virtual string CreatorName { get; set; }
        public virtual Races Race { get; set; }
        public virtual List<Class> Classes { get; set; }
        public virtual Dictionary<string, AbilityScore> AbilityScores { get; set; }
        public virtual List<InventoryItem> Inventory { get; set; }
        public virtual List<CharacterModifier> CharacterModifiers { get; set; }
        public virtual List<Spell> SpellsKnown { get; set; }
        public virtual Sizes Size { get; set; }
        public virtual int MaxHP { get; set; }
        public virtual int CurrentHP { get; set; }
        public virtual double Gold { get; set; }
        public virtual int XPCurrent { get; set; }
        public virtual int XPNext { get; set; }
        public string Languages { get; set; }
        public Alignments Alignment { get; set; }
        public string Deity { get; set; }
        public string Homeland { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string DamageReduction { get; set; }
        public string Resistances { get; set; }
        public string Immunities { get; set; }
        public string Notes { get; set; }

        public Character() : base()
        {
            Classes = new List<Class>();
            AbilityScores = new Dictionary<string, AbilityScore>();
            Inventory = new List<InventoryItem>();
            CharacterModifiers = new List<CharacterModifier>();
            SpellsKnown = new List<Spell>();
            AbilityScores = new Dictionary<string, AbilityScore>()
            {
                {Abilities.Strength.ToString(), new AbilityScore(Abilities.Strength, 10) },
                {Abilities.Constitution.ToString(), new AbilityScore(Abilities.Constitution, 10) },
                {Abilities.Dexterity.ToString(),  new AbilityScore(Abilities.Dexterity, 10) },
                {Abilities.Wisdom.ToString(),  new AbilityScore(Abilities.Wisdom, 10) },
                {Abilities.Intelligence.ToString(), new AbilityScore(Abilities.Intelligence, 10) },
                {Abilities.Charisma.ToString(), new AbilityScore(Abilities.Charisma, 10) },
            };
        }
    }
}

