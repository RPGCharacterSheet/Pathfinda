using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
namespace MongoModels
{
    public abstract class MongoEntityBase
    {
        public ObjectId _id { get; set; }
    }
    public abstract class MongoEntityBase<T> : MongoEntityBase where T : MongoEntityBase
    {

        // T will be Character, or Feat, or whatever
        public static T Get(ObjectId id)
        {
            var collection = Database.Instance.db.GetCollection<T>(typeof(T).Name);
            return collection.Find(doc => doc._id == id).First();

        }
        //overwrite the character currently stored in the database
        public static Task<T> Put(T replacement)
        {
            var collection = Database.Instance.db.GetCollection<T>(typeof(T).Name);
            return collection
                .FindOneAndReplaceAsync<T>(
                    filter: doc => doc._id == replacement._id,
                    replacement: replacement
                );
        }

        // don't let anybody make a Character, or Feat, or Spell. Force them to call Character.Get()
        //What if they want to create a new character?
        protected MongoEntityBase()
        {

        }
    }
}
