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

        public T GetSomeData<T>(string uniqueField, string dataInUniqueField) where T : MongoEntityBase
        {
            var allOfEm = db.GetCollection<T>(typeof(T).Name);
            return allOfEm.Find(doc => doc._id as string == dataInUniqueField).First();
        }

        public bool PutSomeData<T>(T insertOrUpdate) where T : MongoEntityBase
        {
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
