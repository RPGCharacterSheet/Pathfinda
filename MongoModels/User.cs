using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Cryptography;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
namespace MongoModels
{
    class User
    {

        public class UserModel
        {

            public object _id { get; set; } //MongoDb uses this field as identity.

            [BsonId]
            public int ID { get; set; }

            [BsonRepresentation(MongoDB.Bson.BsonType.String)]
            public string UserName { get; set; }

            //[DataType(DataType.Password)]
            [BsonRepresentation(MongoDB.Bson.BsonType.String)]
            public string Password { get; set; }

            // [DataType(DataType.EmailAddress)]
            [BsonRepresentation(MongoDB.Bson.BsonType.String)]
            public string Email { get; set; }
        }

        static HashAlgorithm hashAlgorithm = SHA512.Create();

        private string encodePassword(string user, string password)
        {
            byte[] salt = (byte[])Encoding.Unicode.GetBytes(user);
            List<byte> buffer =
              new List<byte>(Encoding.Unicode.GetBytes(password));
            buffer.AddRange(salt);
            byte[] computedHash = hashAlgorithm.ComputeHash(buffer.ToArray());
            return System.Text.Encoding.UTF8.GetString(computedHash);
        }

        public UserModel newUser(string username, string password)
        {
            var user = new UserModel()
            {
                UserName = username,
                Password = encodePassword(username, password)
            };
            collection.InsertOne(user);
            return user;
        }

        public UserModel getUser(string username, string password)
        {
            return
                (from e in collection.AsQueryable<UserModel>()
                where e.UserName == username && e.Password == encodePassword(username, password)
                select e).First();
        }


        private static MongoModels.Database db;
        private static IMongoCollection<UserModel> collection;
        static User()
        {
            db = Database.Instance;
            collection = db.db.GetCollection<UserModel>("feats");
        }
    }
}
