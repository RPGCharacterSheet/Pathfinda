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

        private Database()
        {
            var connectionString = "mongodb://jogimbel.ddns.net";
            connectionString = "mongodb://192.168.1.185";
            client = new MongoClient(connectionString);
            db = client.GetDatabase("Pathfinda");

        }
    }
}
