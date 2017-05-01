using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalGroupProjectTeam8.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace FinalGroupProjectTeam8.Controllers
{
    public class BankAccountController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: BankAccounts/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "AccountType, Name, Balance")] BankAccount BankAccount)
        {
            if (ModelState.IsValid)
            {
                // Setting the UserID
                string UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                BankAccount.UserID = UserId;

                // Adding the object to the DB
                db.BankAccounts.Add(BankAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(BankAccount);
        }        
    }
}