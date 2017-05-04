using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalGroupProjectTeam8.Models;
using Microsoft.AspNet.Identity;

namespace FinalGroupProjectTeam8.Controllers
{
    [Authorize(Roles = "BankUser")]
    public class PayeeController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Home()
        {
            bool loggedIn = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!loggedIn)
            {
                // If not logged in, redirect to home page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Otherwise, stay here

                // Let's get a list of payees to add to ViewBag
                var payees = from a in db.Payees
                               select a;
                ViewBag.Payees = payees;

                //Finally, return the view
                return View(payees.ToList());
            }
        }
        // GET: /Payee/CreatePayee
        public ActionResult CreatePayee()
        {
            return View();
        }

        // POST: BankAccounts/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePayee([Bind(Include = "PayeeID, Name, Street, City, State, Zip, PhoneNumber, Type")] Payee Payee)
        {
            if (ModelState.IsValid)
            {

                // Ensure we get the right primary key
                var idObject = db.Payees.OrderByDescending(b => b.PayeeID).FirstOrDefault();
                if (idObject == null) Payee.PayeeID = "1";
                else
                {
                    int nextId = Convert.ToInt32(idObject.PayeeID) + 1;
                    String nextIdString = nextId.ToString();
                    Payee.PayeeID = nextIdString;
                }


                // Adding the object to the DB
                db.Payees.Add(Payee);
                db.SaveChanges();

                // Redirect to the right page
                return RedirectToAction("ApplicationSuccess");
            }

            return View();
        }

        public ActionResult CreatePayment()
        {
            return View();
        }

        public ActionResult ApplicationSuccess()
        {
            return View();
        }
    }
}