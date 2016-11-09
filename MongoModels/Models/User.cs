using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
namespace MongoModels.Models
{
    public class User : ModelEntity<UserModel>
    {
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
                (from e in collection.AsQueryable()
                 where e.UserName == username && e.Password == encodePassword(username, password)
                 select e).First();
        }
    }

    public class UserModel : MongoEntityBase
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        static HashAlgorithm hashAlgorithm = SHA512.Create();

    }

}
