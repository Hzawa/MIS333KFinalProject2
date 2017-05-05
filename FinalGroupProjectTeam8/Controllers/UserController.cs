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

        public ActionResult Home(String UserID)
        {
            bool loggedIn = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!loggedIn) {

                // If not logged in, redirect to home page
                return RedirectToAction("Index", "Home");

            } else {
                //Otherwise, stay here

                // If no UserID was passed in, and this is an employee/manager account, they obviously don't want to se this CustomerHome method
                if (UserID == null) {
                    if (User.IsInRole("Manager")) {
                        return RedirectToAction("ManagerHome");
                    }
                    if (User.IsInRole("Employee")) {
                        return RedirectToAction("EmployeeHome");
                    }
                }

                // Let's get a list of accounts to add to ViewBag
                String CurrentUserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                if (UserID == null) UserID = CurrentUserID; // To allow employee access

                // Real quick, check if UserID matches CurrentUserID, only allow progression if CurrentUser is at least an employee
                if (CurrentUserID != UserID) {
                    if (!User.IsInRole("Employee") && !User.IsInRole("Manager"))
                        return RedirectToAction("Error", "Home", new { ErrorMessage = "You don't have permission to be here." });
                }

                // Now get the accounts
                var accounts = from a in db.BankAccounts
                               where a.UserID.Equals(UserID)
                               select a;
                ViewBag.Accounts = accounts;

                // If accounts is empty, redirect
                if (accounts.Count() == 0) return RedirectToAction("ApplyForAccount");

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

        public ActionResult EmployeeHome() {

            // Get the list of users to pass in
            var users = db.Users.Where(u => u.UserType == UserTypeEnum.Customer);

            // Return the view
            return View(users.ToList());
        }

        public ActionResult ManagerHome() {

            // Get unresolved disputes
            var disputes = db.Disputes.Where(d => d.DisputeType == DisputeTypeEnum.Submitted);

            // Get unresolved transactions
            var transactions = db.Transactions.Where(t => t.TransactionStatus == Transaction.TransactionStatusEnum.Pending);

            // Create the view model
            ManagerHomeViewModel ManagerHomeViewModel = new ManagerHomeViewModel();
            ManagerHomeViewModel.Disputes = disputes.ToList();
            ManagerHomeViewModel.Transactions = transactions.ToList();

            return View(ManagerHomeViewModel);
        }

        public ActionResult ManageAccount(String UserID) {

            // Redirect to right place depending on what type of user we currently have
            if (User.IsInRole("Manager")) {

                // Allow the redirect
                return RedirectToAction("Edit", "AppUsers", new { id = UserID });

            }
            if (User.IsInRole("Employee")) {

                // Allow the redirect
                return RedirectToAction("Edit", "AppUsers", new { id = UserID });

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
