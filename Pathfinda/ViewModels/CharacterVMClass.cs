using MongoDB.Driver;
using MongoModels;
using MongoModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinda.ViewModels
{
    public class CharacterVMClass : MongoEntityBase<CharacterVM>
    {
        //Get characters a user owns
        public static List<CharacterVM> getUserCharacters(User.UserModel user)
        {
            //return collection.Find(character => character.Owner == user._id).ToList();
            return (from e in collection.AsQueryable()
                    where e.Owner == user._id
                    select e)
                    .ToList();
        }
        public static List<CharacterVM> getCharactersSharedWith(User.UserModel user)
        {
            return (from character in collection.AsQueryable()
                    where character.Shared.Contains(user._id)
                    select character)
                    .ToList();
        }
        //Overload the Put to require a user
        public static CharacterVM PutSync(CharacterVM character, User.UserModel user)
        {
            if (user._id == character.Owner)
            {
                return collection.FindOneAndReplace<CharacterVM>(
                    c => c._id == character._id,
                    character,
                    new FindOneAndReplaceOptions<CharacterVM, CharacterVM> { IsUpsert = true });
            }
            else
            {
                return null; //Maybe throw error eventually?
            }
        }

        protected CharacterVMClass() : base()
        {
            // initialize a default Character Class
        }
    }
}