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

        }

        private static MongoModels.Database db;
        private static IMongoCollection<CharacterModel> collection;

        public CharacterModel Model()
        {
            return new CharacterModel();
        }



        static Character()
        {
            db = Database.Instance;
            collection = db.db.GetCollection<CharacterModel>("characters");
        }
    }
}
