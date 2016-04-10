using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SRM.Models;
using SRM.Utils;

namespace SRM.Controllers
{
    public class AdminLteController : Controller
    {
        private tbUser user = Singleton.userLogin;
        // GET: AdminLte
        public ActionResult Index()
        {
            //InitAuth();
            if (Session[SessionIndex.userID.ToString()] == null)
            {
                return RedirectToAction("Login", "UserAuth");
            }
            return View();
        }

        public ActionResult Modal()
        {
            return View("UserAuth/_UserProfile");
        }

        public void InitAuth()
        {
            var u = Singleton.GetDatabase().tbUsers.First();
            // case for debug code
            Session[SessionIndex.userID.ToString()] = u.Userid;
            Session[SessionIndex.UserName.ToString()] = u.UserName;
            Session[SessionIndex.Position.ToString()] = "Developer";
            Session[SessionIndex.RegisteredDate.ToString()] = DateTime.Now;
            Session[SessionIndex.isOnlined.ToString()] = true;
            Session[SessionIndex.pwd.ToString()] = u.pwd;
            Session[SessionIndex.userImagePath.ToString()] = Server.MapPath("~") + "Content\\ico\\pro.jpg";
        }
    }
}