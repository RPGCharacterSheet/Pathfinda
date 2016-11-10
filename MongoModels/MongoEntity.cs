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


        public static IMongoCollection<T> collection = Database.Instance.db.GetCollection<T>(typeof(T).Name);
        // T will be Character, or Feat, or whatever
        public static T GetById(ObjectId id)
        {
            return collection.Find(doc => doc._id == id).First();
        }
        //overwrite the character currently stored in the database
        public async Task<T> Put(T puts)
        {
            return await collection
                .FindOneAndReplaceAsync<T>(
                    filter: doc => doc._id == this._id,
                    replacement: (puts),
                    options: new FindOneAndReplaceOptions<T> { IsUpsert= true }
                );
        }
        // don't let anybody make a Character, or Feat, or Spell. Force them to call Character.Create()
        public T Create()
        {
            var createdT = Activator.CreateInstance(typeof(T), true) as T;
            collection.InsertOne(createdT);
            return createdT;
        }
        
        protected MongoEntityBase()
        {

        }
    }
}
