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
            return Json(CharacterClass.GetById(ObjectId(id));
        }

        // POST: Character/Create
        [HttpGet]
        [HttpPost]
        public ActionResult Create(Character character=CharacterClass.Create())
        {
            return Json(CharacterClass.Put(character));
        }

        // GET||Post: Character/Edit
        [HttpGet]
        [HttpPost]
        public ActionResult Edit(Character character)
        {
            return Json(Character.Put(character));
        }

        // GET: Character/Delete/5
        [HttpGet]
        [HttpPost]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            return Json(CharacterClass.Delete(MongoId(id)));
        }
    }
}
