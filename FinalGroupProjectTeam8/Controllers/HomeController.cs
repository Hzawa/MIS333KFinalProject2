using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalGroupProjectTeam8.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            bool loggedIn = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (loggedIn) {
                // If logged in, redirect to user home page
                return RedirectToAction("Home", "User");
            } else {
                // Otherwise continue as normal
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}