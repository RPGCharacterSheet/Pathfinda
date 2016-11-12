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
        public string Name { get; set; }
        public string CreatorName { get; set; }
        public Races Race { get; set; }
        public List<Class> Classes { get; set; }
        public Dictionary<string, AbilityScore> AbilityScores { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public List<CharacterModifier> CharacterModifiers { get; set; }
        public List<Spell> SpellsKnown { get; set; }
        public Sizes Size { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public double Gold { get; set; }
        public int XPCurrent { get; set; }
        public int XPNext { get; set; }
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
            
        }

        private void CreateDummyCharacter()
        {
            Inventory = new List<InventoryItem>();
            AbilityScores = new Dictionary<string, AbilityScore>()
            {
                {Abilities.Strength.ToString(), new AbilityScore(Abilities.Strength, 8) },
                {Abilities.Constitution.ToString(), new AbilityScore(Abilities.Constitution, 10) },
                {Abilities.Dexterity.ToString(),  new AbilityScore(Abilities.Dexterity, 10) },
                {Abilities.Wisdom.ToString(),  new AbilityScore(Abilities.Wisdom, 10) },
                {Abilities.Intelligence.ToString(), new AbilityScore(Abilities.Intelligence, 10) },
                {Abilities.Charisma.ToString(), new AbilityScore(Abilities.Charisma, 10) },
            };
            CharacterModifiers = new List<CharacterModifier>();
            Race = Races.Human;
            Size = Sizes.Medium;
            MaxHP = 10;
            CurrentHP = 10;
        }
    }
}

