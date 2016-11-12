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
        [HttpPost]
        public string Index(string userName, string password)
        {
            Response.ContentType = "application/json";
            User.UserModel user = MongoModels.Models.User.getUser(userName, password);
            if(user == null)
            {
                return @"{""error"": ""authentication failed""}";
            }
            
            return user.ToJson();
        }

        // GET: User/Create
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public string Create(string username, string password, string email="")
        {
            Response.ContentType = "application/json";
            return MongoModels.Models.User.newUser(username, password).ToJson();
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
