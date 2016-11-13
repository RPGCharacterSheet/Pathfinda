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
    public class UserController : Controller
    {
        public static JsonWriterSettings strictWriter = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };

        // GET: User
        [HttpPost]
        public string Index(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password)) return new { _t = "Error", error = "userName or password was not defined" }.ToJson();
            Response.ContentType = "application/json";
            User.UserModel user = MongoModels.Models.User.getUser(userName, password);
            if(user == null)
            {
                return new { error = "authentication failed" }.ToJson(); ;
            }
            user.Password = "";
            user.Email = user.Email ?? "";

            return user.ToJson();
        }

        // GET: User/Create
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public string Create(string username, string password, string email="")
        {
            Response.ContentType = "application/json";
            var user = MongoModels.Models.User.newUser(username, password);
            user.Password = "";
            user.Email = user.Email ?? "";
            return user.ToJson();
        }
        
        public string Lookup(string name)
        {
            Response.ContentType = "application/json";
            return MongoModels.Models.User
                .findUserWithLikeName(name)
                .Select(e => new { UserName = e.UserName, _id = e._id })
                .ToJson(writerSettings: strictWriter);
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
