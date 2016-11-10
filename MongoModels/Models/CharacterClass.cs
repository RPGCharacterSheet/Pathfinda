using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
namespace MongoModels.Models
{
    public class CharacterClass : MongoEntityBase<Character>
    {
        //Get characters a user owns
        public List<Character> getUsersCharacter(User.UserModel user)
        {
            return (from e in collection.AsQueryable() where e.Owner == user._id select e) as List<Character>;
        }

        protected CharacterClass() : base()
        {
            // initialize a default Character Class
        }
    }
}
