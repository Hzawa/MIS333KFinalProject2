using FinalGroupProjectTeam8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace FinalGroupProjectTeam8.Controllers
{
    public class UserController : Controller
    {

        // DB we'll be using for queries
        private AppDbContext db = new AppDbContext();

        public ActionResult Home()
        {
            bool loggedIn = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!loggedIn) {
                // If not logged in, redirect to home page
                return RedirectToAction("Index", "Home");
            } else {
                // Otherwise, stay here

                // Let's get a list of accounts to add to ViewBag
                var customerId = 1;
                var accounts = from a in db.BankAccounts
                               where a.CustomerID.Equals(customerId)
                               select a;
                ViewBag.Accounts = accounts;

                // Finally, return the view
                return View();
            }
        }
    }
}
