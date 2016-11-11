using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
namespace MongoModels.Models
{
    public class CharacterClass : MongoEntityBase<Character>
    {
        //Get characters a user owns
        public static List<Character> getUserCharacters(User.UserModel user)
        {
            return collection.Find(character => character.Owner == user._id).ToList();
                //((from e in collection.AsQueryable() where e.Owner == user._id select e).ToList());
        }

        protected CharacterClass() : base()
        {
            // initialize a default Character Class
        }
    }
}
