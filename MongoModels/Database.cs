using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Core;
namespace MongoModels
{
    public class Database
    {
        public MongoClient client;
        public IMongoDatabase db;
        private static Database _instance;
        public static Database Instance
        {
            get { return (_instance ?? (_instance = new Database())); }
        }

        public T GetSomeData<T>(string uniqueField, ObjectId dataInUniqueField) where T : MongoEntityBase
        {
            var allOfEm = db.GetCollection<T>(typeof(T).Name);
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

        public bool PutSomeData<T>(T insertOrUpdate) where T : MongoEntityBase
        {
            //In shell this is db[typeof(T)].update(query, objectToOverwrite)
            //In c#

            db
                .GetCollection<T>(typeof(T).Name)
                .ReplaceOne<T>(
                    filter: doc => doc._id == insertOrUpdate._id,
                    replacement: insertOrUpdate
                );

            bool worked = false;
            // I have no idea
            return worked;
        }

        private Database()
        {
            var connectionString = "mongodb://jogimbel.ddns.net";
            client = new MongoClient(connectionString);
            db = client.GetDatabase("Pathfinda");

        }
    }
}
