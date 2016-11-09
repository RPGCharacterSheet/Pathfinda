using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoModels
{
    public class ModelEntity <T> where T : MongoEntityBase
    {
        static IMongoCollection<T> collection;
        static Database db;

        public T GetSomeData(string uniqueField, ObjectId dataInUniqueField)
        {
            var allOfEm = db.db.GetCollection<T>(typeof(T).Name);
            return allOfEm.Find(doc => doc._id == dataInUniqueField).First();

            //or this could work if you like linq as better
            //return (from e in allOfEm.AsQueryable<t>()
            // where e._id == dataInUniqueField
            // select e).First();


            //or if you really want to use the uniqueField
            //return AllOfEm.find(BsonDocument{ { uniqueField, dataInUnqiueField } }).First();


            //in the shell this kind of query looks like
            //db[typeof(T)].find({ 
            //  [uniqueField]: dataInUniqueField
            //})

            //mongo is based off of javascript
            //and in js, 
            //db['characters']
            //and
            //db.characters
            //are the same get command.
            //hence db[typeof(T)]
            //Will be the same as
            //db.Character.find()
        }

        public T PutSomeData(T insertOrUpdate)
        {
            //In shell this is db[typeof(T)].update(query, objectToOverwrite)
            //In c#

            return db.db
                .GetCollection<T>(typeof(T).Name)
                .FindOneAndReplace<T>(
                    filter: doc => doc._id == insertOrUpdate._id,
                    replacement: insertOrUpdate
                );
        }

        static ModelEntity()
        {
            db = Database.Instance;
            collection = db.db.GetCollection<T>(typeof(T).Name);
        }
    }
}
