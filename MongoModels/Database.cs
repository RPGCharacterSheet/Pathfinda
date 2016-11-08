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
            get { return (_instance == null) ? _instance = new Database() : _instance; }
        }

        private Database()
        {
            var connectionString = "mongodb://jogimbel.ddns.net";
            client = new MongoClient(connectionString);
            db = client.GetDatabase("Pathfinda");

        }
    }
}
