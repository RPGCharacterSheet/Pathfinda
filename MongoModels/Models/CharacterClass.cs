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
        public static List<Character> getUserCharacters(User.UserModel user)
        {
            //return collection.Find(character => character.Owner == user._id).ToList();
            return (from e in collection.AsQueryable()
                    where e.Owner == user._id
                    select e)
                    .ToList();
        }
        public static List<Character> getCharactersSharedWith(User.UserModel user)
        {
            return (from character in collection.AsQueryable()
                    where character.Shared.Contains(user._id)
                    select character)
                    .ToList();
        }
        //Overload the Put to require a user
        public static Character PutSync(Character character, User.UserModel user)
        {
            if(user._id == character.Owner)
            {
                return collection.FindOneAndReplace<Character>(
                    c => c._id == character._id, 
                    character, 
                    new FindOneAndReplaceOptions<Character, Character> { IsUpsert = true });
            }
            else
            {
                return null; //Maybe throw error eventually?
            }
        }

        protected CharacterClass() : base()
        {
            // initialize a default Character Class
        }
    }
}
