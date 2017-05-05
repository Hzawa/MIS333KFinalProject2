﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalGroupProjectTeam8.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Data.Entity;
using static FinalGroupProjectTeam8.Models.Transaction;

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

                // Store this for later
                Decimal initialDeposit = BankAccount.Balance;
                if (initialDeposit < 0) {
                    return RedirectToAction("Error", "Home", new { ErrorMessage = "Cannot have negative initial deposit." });
                }

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
                    if (initialDeposit > 5000)
                    {
                        // Manager must approve if deposit > $5000    
                        BankAccount.Balance = 0;
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

                /**
                 * Add initial transaction as a deposit
                 */

                // Give this transaction the right primary key
                String TransactionID = "";
                var idObjectT = db.Transactions.OrderByDescending(b => b.TransactionID).FirstOrDefault();
                if (idObject == null) TransactionID = "1000000000";
                else
                {
                    int nextId = Convert.ToInt32(idObjectT.TransactionID) + 1;
                    String nextIdString = nextId.ToString();
                    TransactionID = nextIdString;
                }

                // Create Overdraft fee
                Transaction Transaction = new Models.Transaction();

                // Give this transaction the right primary key
                Transaction.TransactionID = TransactionID;

                // Other properties
                Transaction.TransactionType = Transaction.TransactionTypeEnum.Deposit;
                Transaction.Description = "Initial Deposit";
                Transaction.Date = DateTime.Now;
                Transaction.BankAccountID = BankAccount.BankAccountID;
                Transaction.Amount = initialDeposit;
                if (initialDeposit > 5000)
                    Transaction.TransactionStatus = Transaction.TransactionStatusEnum.Pending;
                else
                    Transaction.TransactionStatus = Transaction.TransactionStatusEnum.Approved;
                db.Transactions.Add(Transaction);
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

        //method to get all transactions
        public SelectList GetAllTransactions()
        {
            var query = from c in db.Transactions
                        orderby c.TransactionType
                        select c.TransactionType;

            List<Transaction.TransactionTypeEnum> allTransactions = query.Distinct().ToList();

            Transaction.TransactionTypeEnum NoChoice = new Transaction.TransactionTypeEnum() { };
            allTransactions.Add(NoChoice);

            SelectList allTransactionsList = new SelectList(allTransactions);

            return allTransactionsList;
        }

        //search
        public ActionResult Details(String BankAccountID, String TransactionID, String Description, TransactionTypeEnum? TransactionType, Decimal? AmountLowerBound, Decimal? AmountUpperBound, DateTime? DateLowerBound, DateTime? DateUpperBound) {

            List<Transaction> SelectedTransactions = new List<Transaction>();

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
                if (!User.IsInRole("Employee") && User.IsInRole("Manager"))
                    return RedirectToAction("Error", "Home", new { ErrorMessage = "This is not your account." });
            }

            // Ensure it's active
            if (BankAccount.Active == false) {
                return RedirectToAction("Error", "Home", new { ErrorMessage = "This account is not active. Contact a manager for further advising." });
            }

            // Get transactions associated with this bank account
            var transactions = from t in db.Transactions
                               where t.BankAccountID == BankAccount.BankAccountID
                               select t;
            List<Transaction> AllTransactions = transactions.ToList();


            /**
             * START filtering transactions
             */

            // Transaction ID filter
            if (TransactionID != null && TransactionID != "") {
                transactions = transactions.Where(t => t.TransactionID.Contains(TransactionID));
            }

            // Transaction type filter
            if (TransactionType != null && TransactionType != TransactionTypeEnum.All) {
                transactions = transactions.Where(t => t.TransactionType == TransactionType);
            }

            // Description filter
            if (Description != null && Description != "")
            {
                transactions = transactions.Where(t => t.Description.Contains(Description));
            }

            // Amount filter
            if (AmountLowerBound != null && AmountLowerBound != 0.00m && AmountUpperBound != null && AmountUpperBound != 0.00m)
            {
                transactions = transactions.Where(t => t.Amount > AmountLowerBound && t.Amount < AmountUpperBound);
            }
            else if (AmountLowerBound != null && AmountLowerBound != 0.00m)
            {
                transactions = transactions.Where(t => t.Amount > AmountLowerBound);
            }
            else if (AmountUpperBound != null && AmountUpperBound != 0.00m)
            {
                transactions = transactions.Where(t => t.Amount < AmountUpperBound);
            }

            // Date filter
            if (DateLowerBound != null && DateLowerBound != DateTime.MinValue && DateUpperBound != null && DateUpperBound != DateTime.MinValue)
            {
                transactions = transactions.Where(t => t.Date > DateLowerBound);
                transactions = transactions.Where(t => t.Date < DateUpperBound);
            }
            else if (AmountLowerBound != null && DateLowerBound != DateTime.MinValue)
            {
                transactions = transactions.Where(t => t.Date > DateLowerBound);
            }
            else if (AmountUpperBound != null && DateUpperBound != DateTime.MinValue)
            {
                transactions = transactions.Where(t => t.Date < DateUpperBound);
            }

            // Finally, sort the transactions
            transactions = transactions.OrderBy(t => t.TransactionID).ThenBy(t => t.TransactionType).ThenBy(t => t.Description).ThenBy(t => t.Amount);

            /**
             * END filtering transactions
             */

            // Create the ViewModel
            var model = new BankAccountDetailsViewModel { BankAccountID = BankAccount.BankAccountID, BankAccount = BankAccount, Transactions = transactions.ToList() };

            // Employees/Managers cannot create disputes
            if (User.IsInRole("Employee") || User.IsInRole("Manager"))
            {
                model.AllowDisputeCreation = false;
            }
            else {
                model.AllowDisputeCreation = true;
            }

            // Otherwise we're good, make any changes we need to
            if (BankAccount.AccountType == BankAccount.BankAccountTypeEnum.CheckingAccount) {
                
            } else if (BankAccount.AccountType == BankAccount.BankAccountTypeEnum.SavingsAccount) {

            } else if (BankAccount.AccountType == BankAccount.BankAccountTypeEnum.IRA) {

            } else if (BankAccount.AccountType == BankAccount.BankAccountTypeEnum.StockPortfolio) {

            }

            SelectedTransactions = transactions.ToList();

            // count # of records
            ViewBag.TransactionCount = SelectedTransactions.Count();
            ViewBag.TotalTransactionCount = AllTransactions.Count();

            // First, ensure this bank account belongs to current customer
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(BankAccountDetailsViewModel BankAccountDetailsViewModel)
        {
            return RedirectToAction
                ("Details", "BankAccount",
                new {
                    BankAccountID = BankAccountDetailsViewModel.BankAccountID,
                    Description = BankAccountDetailsViewModel.DescriptionFilter,
                    TransactionID = BankAccountDetailsViewModel.TransactionID,
                    TransactionType = BankAccountDetailsViewModel.TransactionType,
                    AmountLowerBound = BankAccountDetailsViewModel.AmountLowerBound,
                    AmountUpperBound = BankAccountDetailsViewModel.AmountUpperBound,
                    DateLowerBound = BankAccountDetailsViewModel.DateLowerBound,
                    DateUpperBound = BankAccountDetailsViewModel.DateUpperBound,
                });
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

            ModelState.Clear();
            if (TryValidateModel(EditingBankAccount))
            {
                db.Entry(EditingBankAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Home", "User");
            }
            
            return View(bankAccount);
        }

    }
}