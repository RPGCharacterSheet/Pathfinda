using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
namespace MongoModels.Models
{
    public class User : MongoEntityBase<User.UserModel>
    {
        //Model to store in database
        public class UserModel : MongoEntityBase
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
        }

        static HashAlgorithm hashAlgorithm = SHA512.Create();

        private static string encodePassword(string user, string password)
        {
            byte[] salt = (byte[])Encoding.Unicode.GetBytes(user);
            List<byte> buffer =
              new List<byte>(Encoding.Unicode.GetBytes(password));
            buffer.AddRange(salt);
            byte[] computedHash = hashAlgorithm.ComputeHash(buffer.ToArray());
            return System.Text.Encoding.UTF8.GetString(computedHash);
        }

        public static List<UserModel> findUserWithLikeName(string name)
        {
            return (from e in collection.AsQueryable() where e.UserName.Contains(name) select e).ToList();
        }

        public static UserModel newUser(string username, string password)
        {
            var user = new UserModel()
            {
                UserName = username,
                Password = encodePassword(username, password)
            };
            collection.InsertOne(user);
            return user;
        }

        public static UserModel getUser(string username, string password)
        {
            var cursor = (
                from e in collection.AsQueryable<UserModel>()
                where e.UserName == username && e.Password == encodePassword(username, password)
                select e
            );
            return cursor.Count() > 0 ? cursor.First() : null;
        }
    }
}
