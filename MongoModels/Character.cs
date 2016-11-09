using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
namespace MongoModels
{
    class Character
    {
        public class CharacterModel
        {
            public BsonObjectId owner { get; private set; }
            public Dictionary<Abilities, AbilityScore> AbilityScores { get; set; }
            public List<Gear> Inventory { get; set; }
            public List<MiscEffect> MiscEffects { get; set; }
            public Races Race { get; set; }
            public Sizes Size { get; set; }

            public int MaxHP { get; set; }
            public int CurrentHP { get; set; }

            #region Specified by the player and not used in any calculations
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
            public int Strength { get { return AbilityScores[Abilities.Strength].Score; } }
            public int Dexterity { get { return AbilityScores[Abilities.Dexterity].Score; } }
            public int Constitution { get { return AbilityScores[Abilities.Constitution].Score; } }
            public int Wisdom { get { return AbilityScores[Abilities.Wisdom].Score; } }
            public int Intelligence { get { return AbilityScores[Abilities.Intelligence].Score; } }
            public int Charisma { get { return AbilityScores[Abilities.Charisma].Score; } }
            #endregion
            public CharacterModel() { }
            public CharacterModel(BsonObjectId own)
            {
                owner = own;
            }
        }

        private static MongoModels.Database db;
        private static IMongoCollection<CharacterModel> collection;

        public CharacterModel Model(BsonObjectId owner = null)
        {
            return new CharacterModel(owner);
        }

        static Character()
        {
            db = Database.Instance;
            collection = db.db.GetCollection<CharacterModel>("characters");
        }
    }
}
