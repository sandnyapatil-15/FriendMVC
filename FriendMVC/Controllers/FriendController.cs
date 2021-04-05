using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendMVC.Models;
using System.Web.Security;

namespace FriendMVC.Controllers
{
    public class FriendController : Controller
    {
        LTIMVCEntities db = new LTIMVCEntities();
        // GET: Friend
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Friend F)
        {
            if (ModelState.IsValid)
            {
                db.Friends.Add(F);
                var res = db.SaveChanges();
                if (res > 0)
                    ModelState.AddModelError("", "Added a new Friend");
            }
                return View();
        }
        public ActionResult GetFriend()
        {
            var data = db.Friends.ToList();
            return View(data);

        }

        [HttpGet]
        public ActionResult UpdateFriend(int id)
        {
            var data = db.Friends.Where(x => x.FriendID == id).SingleOrDefault();
            return View(data);
        }

        [HttpPost]

        public ActionResult UpdateFriend()
        {
            int id = Convert.ToInt32(Request.Form["FriendID"]);
            var olddata = db.Friends.Where(x => x.FriendID == id).SingleOrDefault();
            var newname = Request.Form["FriendName"];
            var newplace = Request.Form["Place"];
            olddata.FriendName = newname;
            olddata.Place = newplace;
            var res = db.SaveChanges();
            if (res > 0)
                return RedirectToAction("GetFriend");
            return RedirectToAction("Index");

        }

        [HttpGet]

        public ActionResult DeleteFriend(int id)
        {
            var data = db.Friends.Where(x => x.FriendID == id).SingleOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult DeleteFriend()
        {
            int id = Convert.ToInt32(Request.Form["FriendID"]);
            var delrow = db.Friends.Where(x => x.FriendID == id).SingleOrDefault();
            db.Friends.Remove(delrow);
            var res = db.SaveChanges();
            if (res > 0)
                return RedirectToAction("GetFriend");
            return RedirectToAction("GetFriend");

        }
    }
}