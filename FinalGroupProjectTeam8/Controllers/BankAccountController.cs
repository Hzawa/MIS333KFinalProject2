using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalGroupProjectTeam8.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Data.Entity;

namespace FinalGroupProjectTeam8.Controllers
{
    public class BankAccountController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: /Account/Register
        [Authorize(Roles = "BankUser, Manager, Employee")]
        public ActionResult Register()
        {
            return View();
        }

        // POST: BankAccounts/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "BankAccountID, AccountType, Name, Balance")] BankAccount BankAccount)
        {
            if (ModelState.IsValid)
            {
                // Setting the UserID
                string UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                BankAccount.UserID = UserId;

                // Different behavior and checks depending on account type
                BankAccount.BankAccountTypeEnum BankAccountType = BankAccount.AccountType;

                // Only 1 IRA account
                if (BankAccountType == BankAccount.BankAccountTypeEnum.IRA)
                {

                    // Check if this user already has an IRA account
                    var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var IRAAccounts = db.BankAccounts.Where(b => b.UserID == userId).Where(b => b.AccountType == BankAccount.BankAccountTypeEnum.IRA);

                    // If we have any results, that means this user already has an IRA account
                    if (IRAAccounts.ToList().Count() > 0)
                    {
                        return RedirectToAction("Error", "Home", new { ErrorMessage = "You can only have one IRA account." });
                    }

                }

                // Only 1 stock portfolio account
                if (BankAccountType == BankAccount.BankAccountTypeEnum.StockPortfolio)
                {

                    // Check if this user already has an IRA account
                    var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var StockPortfolioAccounts = db.BankAccounts.Where(b => b.UserID == userId).Where(b => b.AccountType == BankAccount.BankAccountTypeEnum.StockPortfolio);

                    // If we have any results, that means this user already has an StockPortfolio account
                    if (StockPortfolioAccounts.ToList().Count() > 0)
                    {
                        return RedirectToAction("Error", "Home", new { ErrorMessage = "You can only have one Stock Portfolio account." });
                    }

                }

                // Different status if deposit too big
                BankAccount.Active = true;
                if (BankAccountType == BankAccount.BankAccountTypeEnum.IRA || BankAccountType == BankAccount.BankAccountTypeEnum.CheckingAccount)
                {
                    // Check to see if initial deposit greater than $5000
                    Decimal initialDeposit = BankAccount.Balance;
                    if (initialDeposit > 5000)
                    {
                        // Manager must approve if deposit > $5000    
                        BankAccount.Active = false;
                    }
                }

                // If account is checkings or savings provide default name
                if (BankAccountType == BankAccount.BankAccountTypeEnum.CheckingAccount)
                {
                    if (BankAccount.Name == "" || BankAccount.Name == null) BankAccount.Name = "Longhorn Checking";
                }
                else if (BankAccountType == BankAccount.BankAccountTypeEnum.SavingsAccount)
                {
                    if (BankAccount.Name == "" || BankAccount.Name == null) BankAccount.Name = "Longhorn Savings";
                }

                // Ensure we get the right primary key
                var idObject = db.BankAccounts.OrderByDescending(b => b.BankAccountID).FirstOrDefault();
                if (idObject == null) BankAccount.BankAccountID = "1000000000";
                else {
                    int nextId = Convert.ToInt32(idObject.BankAccountID) + 1;
                    String nextIdString = nextId.ToString();
                    BankAccount.BankAccountID = nextIdString;
                }

                // Adding the object to the DB
                db.BankAccounts.Add(BankAccount);
                db.SaveChanges();

                // Redirect to the right page
                return RedirectToAction("ApplicationSuccess");
            }

            return View(BankAccount);
        }

        public ActionResult ApplicationSuccess()
        {
            return View();
        }

        public ActionResult Details(String BankAccountID) {

            // Query for given bank account ID
            var accounts = from a in db.BankAccounts
                           where a.BankAccountID.Equals(BankAccountID)
                           select a;

            // If it doesn't exist, return 404
            if (accounts.Count() == 0) {
                return RedirectToAction("Error", "Home", new { ErrorMessage = "Account does not exist." });
            }
            BankAccount BankAccount = accounts.First();

            // Ensure it belongs to current user
            string UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (BankAccount.UserID != UserId) {
                return RedirectToAction("Error", "Home", new { ErrorMessage = "This is not your account." });
            }

            // Get transactions associated with this bank account
            var transactions = from t in db.Transactions
                               where t.BankAccountID == BankAccount.BankAccountID
                               select t;

            // Create the ViewModel
            var model = new BankAccountDetailsViewModel { BankAccount = BankAccount, Transactions = transactions.ToList() };

            // Otherwise we're good, make any changes we need to
            if (BankAccount.AccountType == BankAccount.BankAccountTypeEnum.CheckingAccount) {
                
            } else if (BankAccount.AccountType == BankAccount.BankAccountTypeEnum.SavingsAccount) {

            } else if (BankAccount.AccountType == BankAccount.BankAccountTypeEnum.IRA) {

            } else if (BankAccount.AccountType == BankAccount.BankAccountTypeEnum.StockPortfolio) {

            }

            // First, ensure this bank account belongs to current customer
            return View(model);
        }

        // GET: BankAccounts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BankAccountID,Name")] BankAccount bankAccount)
        {

            // We only want to edit certain fields
            BankAccount EditingBankAccount = db.BankAccounts.Find(bankAccount.BankAccountID);
            EditingBankAccount.Name = bankAccount.Name;

            if (ModelState.IsValid)
            {
                db.Entry(EditingBankAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Home", "User");
            }
            
            return View(bankAccount);
        }

    }
}