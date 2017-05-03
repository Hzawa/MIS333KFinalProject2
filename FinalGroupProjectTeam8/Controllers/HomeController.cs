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

        public ActionResult Error(String ErrorMessage) {
            ViewBag.ErrorMessage = ErrorMessage;
            return View();
        }
    }
}