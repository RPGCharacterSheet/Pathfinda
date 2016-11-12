using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoModels.Models;
using MongoDB.Bson;
namespace WebAPI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(string userName, string password)
        {
            User.UserModel user = MongoModels.Models.User.getUser(userName, password);
            if(user == null)
            {
                return new HttpNotFoundResult();
            }
            return Json(user);
        }

        // GET: User/Create
        [HttpGet]
        [HttpPost]
        public ActionResult Create(string username, string password, string email="")
        {
            return Json(MongoModels.Models.User.newUser(username, password));
        }
        
        // GET: User/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    return View();
        //}

        //// POST: User/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
