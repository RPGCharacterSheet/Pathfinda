using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
namespace MongoModels
{
    public abstract class MongoEntityBase { }
    public abstract class MongoEntityBase<T> : MongoEntityBase where T : MongoEntityBase
    {
        public ObjectId _id { get; set; }

        // T will be Character, or Feat, or whatever
        public static T Get(string someParametersToFindItInTheDatabase)
        {
            bool foundRecordInMongo = someParametersToFindItInTheDatabase == "whatever";
            if (foundRecordInMongo)
            {
                // return the one from Mongo, not default(T)
                return default(T);


                //return collection.Find(doc => doc._id == dataInUniqueField).First();

                //        //or this could work if you like linq as better
                //        //return (from e in allOfEm.AsQueryable<t>()
                //        // where e._id == dataInUniqueField
                //        // select e).First();


                //        //or if you really want to use the uniqueField
                //        //return AllOfEm.find(BsonDocument{ { uniqueField, dataInUnqiueField } }).First();


                //        //in the shell this kind of query looks like
                //        //db[typeof(T)].find({ 
                //        //  [uniqueField]: dataInUniqueField
                //        //})

                //        //mongo is based off of javascript
                //        //and in js, 
                //        //db['characters']
                //        //and
                //        //db.characters
                //        //are the same get command.
                //        //hence db[typeof(T)]
                //        //Will be the same as
                //        //db.Character.find()

            }
            else
            {
                // return a fresh one
                return Activator.CreateInstance(typeof(T), true) as T;
            }
        }

        public void Put()
        {
            //        //In shell this is db[typeof(T)].update(query, objectToOverwrite)
            //        //In c#

            //        return collection
            //            .FindOneAndReplace<T>(
            //                filter: doc => doc._id == insertOrUpdate._id,
            //                replacement: insertOrUpdate
            //            );
        }

        // don't let anybody make a Character, or Feat, or Spell. Force them to call Character.Get()
        protected MongoEntityBase()
        {

        }
    }
}
