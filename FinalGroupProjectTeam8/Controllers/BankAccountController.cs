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

                //Set PK value
                //BankAccount.BankAccountID = 1000020;
                //BankAccount bankAccountToChange = db.BankAccounts.Max(allBankAccountsList);


                // Different behavior and checks depending on account type
                BankAccount.BankAccountTypeEnum BankAccountType = BankAccount.AccountType;
                if (BankAccountType == BankAccount.BankAccountTypeEnum.IRA)
                {

                    // Check if this user already has an IRA account
                    var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var accounts = from a in db.BankAccounts
                                   where a.UserID.Equals(userId)
                                   where a.AccountType.Equals(BankAccountType)
                                   select a;

                    // If we have any results, that means this user already has an IRA account
                    if (accounts.Count() > 0)
                    {
                        // Redirect

                    }

                    else if (BankAccountType == BankAccount.BankAccountTypeEnum.IRA || BankAccountType == BankAccount.BankAccountTypeEnum.CheckingAccount)
                    {
                        // Check to see if initial deposit greater than $5000
                        Decimal initialDeposit = BankAccount.Balance;
                        if (initialDeposit > 5000)
                        {
                            // Manager must approve if deposit > $5000    
                        }
                    }

                    //If account is checkings or savings provide default name
                    if (BankAccountType == BankAccount.BankAccountTypeEnum.CheckingAccount)
                    {
                        BankAccount.Name = "Longhorn Checking";
                    }
                    else if (BankAccountType == BankAccount.BankAccountTypeEnum.SavingsAccount)
                    {
                       BankAccount.Name = "Longhorn Savings";
                    }

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

    }
}