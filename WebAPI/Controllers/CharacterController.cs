using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoModels.Models;
using MongoDB.Bson;
namespace WebAPI.Controllers
{
    public class CharacterController : Controller
    {

        // GET: Character/5d37a94d...
        public string Index(string id=null, string user=null)
        {
            Response.ContentType = "application/json";
            return (id != null)
                ? CharacterClass.GetById(new ObjectId(id)).ToJson()
                : (user != null)
                    ? CharacterClass.getUserCharacters(MongoModels.Models.User.GetById(new ObjectId(user))).ToJson()
                    : @"{""Error"": ""Either id or user needs to be set in query""}";
        }

        // POST: Character/Create
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public string Create(string user, Character character=null)
        {
            Response.ContentType = "application/json";
            if (user == null)
            {
                return new { error = "user must be defined" }.ToJson();
            }
            if (character == null)
            {
                character = CharacterClass.Create();
            }
            if(character._id.Pid == 0)
            {

                character._id = CharacterClass.Create()._id;

            }
            if(character.Owner.Pid == 0)
            {

                character.Owner = new ObjectId(user);
            }
            CharacterClass.PutSync(character, MongoModels.Models.User.GetById(new ObjectId(user)));
            return character.ToJson();
        }

        // GET||Post: Character/Edit
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public string Edit(Character character, User.UserModel user)
        {
            Response.ContentType = "application/json";
            CharacterClass.PutSync(character, user);
            return character.ToJson();
        }

        // GET: Character/Delete/5
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post | HttpVerbs.Delete)]
        public ActionResult Delete(string id)
        {
            return Json(id);//CharacterClass.Delete(new ObjectId(id)));
        }
    }
}
