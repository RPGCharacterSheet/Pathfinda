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
        public ActionResult Index(string id)
        {
            return Json(CharacterClass.GetById(new ObjectId(id)));
        }

        // POST: Character/Create
        [HttpGet]
        [HttpPost]
        public ActionResult Create(Character character)
        {
            if (character == null)
            {
                character = CharacterClass.Create();
            }
            return Json(CharacterClass.Put(character));
        }

        // GET||Post: Character/Edit
        [HttpGet]
        [HttpPost]
        public ActionResult Edit(Character character, User.UserModel user)
        {
            return Json(CharacterClass.PutSync(character, user));
        }

        // GET: Character/Delete/5
        [HttpGet]
        [HttpPost]
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            return Json(id);//CharacterClass.Delete(new ObjectId(id)));
        }
    }
}
