using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalGroupProjectTeam8.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Data.Entity;


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
                var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                AppUser AppUser = db.Users.Find(userId);

                //Finally, return the view
                return View(AppUser.Payees.ToList());
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

        //GET: /Payee/AddPayee
        [HttpGet]
        public ActionResult AddPayee()
        {
            ViewBag.AllPayees = GetAllPayees();
            return View();
        }

        //create list of all current payees
        public SelectList GetAllPayees()
        {
            string id = User.Identity.GetUserId();
            AppUser user = db.Users.Find(id);
            List<Payee> other = (((db.Payees).ToList()).Except(user.Payees)).ToList();
            SelectList list = new SelectList(other, "PayeeID", "Name");
            return list;
        }

        // POST: /Payee/AddPayee (show drop down list of all current payees to add)
        [HttpPost]
        public ActionResult AddPayee(string[] AllPayees)
        {
            string userId = User.Identity.GetUserId();
            AppUser user = db.Users.Find(userId);

            if (ModelState.IsValid)
            {
                if (AllPayees != null)
                {
                    foreach (string PayeeID in AllPayees)
                    {
                        Payee addPayee = db.Payees.Find(PayeeID);
                        user.Payees.Add(addPayee);
                    }
                }
                db.SaveChanges();
                // Redirect to the right page
                return RedirectToAction("ApplicationSuccess");
            }
            ViewBag.AllPayees = GetAllPayees();
            return View();
        }
        
        // GET: /Payee/CreatePayment
        public ActionResult CreatePayment(string id)
        {

            // We need a list of checking/savings accounts
            string UserID = User.Identity.GetUserId();
            var accounts = db.BankAccounts.Where(a => a.UserID == UserID).Where(a => (a.AccountType == BankAccount.BankAccountTypeEnum.CheckingAccount) || (a.AccountType == BankAccount.BankAccountTypeEnum.SavingsAccount));
            ViewBag.BankAccountID = new SelectList(accounts, "BankAccountID", "NameWithBalance");

            // Pass in the ViewModel 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePayment([Bind(Include = "BankAccountID,Amount,Date,Description")] Payment payment)
        {

            // Give this dispute the right primary key
            var idObject = db.Transactions.OrderByDescending(b => b.TransactionID).FirstOrDefault();
            if (idObject == null) payment.TransactionID = "1";
            else
            {
                int nextId = Convert.ToInt32(idObject.TransactionID) + 1;
                String nextIdString = nextId.ToString();
                payment.TransactionID = nextIdString;
            }

            /**
             * Validation here
             */

            // Do validation here
            if (payment.Amount <= 0)
            {
                return RedirectToAction("Error", "Home", new { ErrorMessage = "Please deposit an amount greater than 0." });
            }

            /**
             * Any changes here
             */
            // Any changes
            payment.TransactionStatus = Transaction.TransactionStatusEnum.Approved;

            // Must update the balance
            BankAccount BankAccount = db.BankAccounts.Find(payment.BankAccountID);
            BankAccount.Balance = BankAccount.Balance - payment.Amount;
            db.SaveChanges();

            /**
             * Finally save it to the database
             */

            // Actually saving it           
            if (ModelState.IsValid)
            {
                db.Transactions.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Success", "Transactions", new { Message = "Your transaction was successfully created." });
            }

            string UserID = User.Identity.GetUserId();
            var accounts = db.BankAccounts.Where(a => a.UserID == UserID).Where(a => (a.AccountType == BankAccount.BankAccountTypeEnum.CheckingAccount) || (a.AccountType == BankAccount.BankAccountTypeEnum.SavingsAccount));
            ViewBag.BankAccountID = new SelectList(accounts, "BankAccountID", "NameWithBalance");
            return View(payment);
        }

        // GET: /Payee/ApplicationSuccess
        public ActionResult ApplicationSuccess()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            return View();
        }

        // GET: Payees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payee @payee = db.Payees.Find(id);
            if (payee == null)
            {
                return HttpNotFound();
            }
            //allow users to edit their own user info, but not others
            //if (member.Id != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            return View(@payee);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PayeeID,Name,Street,City,State,Zip,PhoneNumber")] Payee @payee)
        {
            if (ModelState.IsValid)
            {
                //Find associated payee
                Payee payeeToChange = db.Payees.Find(@payee.PayeeID);

                //update rest of fields
                payeeToChange.Name = @payee.Name;
                payeeToChange.Street = @payee.Street;
                payeeToChange.City = @payee.City;
                payeeToChange.State = @payee.State;
                payeeToChange.Zip = @payee.Zip;
                payeeToChange.PhoneNumber = @payee.PhoneNumber;

                db.Entry(payeeToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Home");
            }

            return View(@payee);
        }

    }
}