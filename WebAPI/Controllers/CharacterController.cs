using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoModels.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
namespace WebAPI.Controllers
{
    public class CharacterController : Controller
    {
        static JsonWriterSettings strictWriter = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
        // GET: Character/5d37a94d...
        public string Index(string id=null, string user=null)
        {
            Response.ContentType = "application/json";
            return (id != null)
                ? CharacterClass.GetById(new ObjectId(id)).ToJson(writerSettings: strictWriter)
                : (user != null)
                    ? CharacterClass
                        .getUserCharacters(MongoModels.Models.User.GetById(new ObjectId(user)))
                        .ToJson(writerSettings: strictWriter)
                    : new { Error = "Either id or user needs to be set in query"}.ToJson(writerSettings: strictWriter);
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
            return character.ToJson(writerSettings: strictWriter);
        }

        // GET||Post: Character/Edit
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public string Edit(Character character, string user)
        {
            Response.ContentType = "application/json";
            CharacterClass.PutSync(character, MongoModels.Models.User.GetById(new ObjectId(user)));
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
