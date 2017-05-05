using FinalGroupProjectTeam8.Models;
using Microsoft.AspNet.Identity;
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
                //Otherwise, stay here

                // Let's get a list of accounts to add to ViewBag
                var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                var accounts = from a in db.BankAccounts
                               where a.UserID.Equals(userId)
                               select a;
                ViewBag.Accounts = accounts;

                //Finally, return the view
                return View(accounts.ToList());
            }
        }

        public ActionResult ApplyForAccount() {
            return RedirectToAction("Register", "BankAccount");
        }

        public ActionResult ManageOwnAccount() {
            String CurrentUserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            return ManageAccount(CurrentUserID);
        }

        public ActionResult ManageAccount(String UserID) {

            // Redirect to right place depending on what type of user we currently have
            if (User.IsInRole("Manager")) {

            }
            if (User.IsInRole("Employee")) {

            }

            // We now assume we're working with a customer, just quickly verify they're allowed
            String CurrentUserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (CurrentUserID != UserID) {
                return RedirectToAction("Error", "Home", new { ErrorMessage = "You don't have permission to be here." });
            }

            // We're good, just redirect to customer details screen
            return RedirectToAction("Edit", "AppUsers", new { id = UserID });

        }

    }
}
