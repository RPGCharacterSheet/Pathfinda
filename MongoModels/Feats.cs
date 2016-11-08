using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
namespace MongoModels
{
    class Feats
    {
        private static MongoModels.Database db;
        private static IMongoCollection<BsonDocument> collection;
        static Feats()
        {
            db = Database.Instance;
            collection = db.db.GetCollection<BsonDocument>("feats");
        }

        public List<BsonDocument> Find(BsonDocument query)
        {
            return collection.Find(query) as List<BsonDocument>;
        }

        public List<BsonDocument> FindAlike(string name)
        {
            return Find(new BsonDocument { { "name", new BsonDocument { { "$regex", name }, { "$options", "i" } } } });
        }
    }
}
