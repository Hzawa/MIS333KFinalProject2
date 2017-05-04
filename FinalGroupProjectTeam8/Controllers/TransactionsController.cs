using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalGroupProjectTeam8.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;

namespace FinalGroupProjectTeam8.Controllers
{
    //enum properties
    public enum TransactionType { All, Deposit, Withdrawal, Transfer, Bill, Fee }

    //public enum CustomAmounts { FirstHundred, SecondHundred, ThirdHundred, Over300 }

    public enum SortOrder { Ascending, Descending }

    public class TransactionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

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

        //advanced search method
        public ActionResult SearchResults(String SearchString, String Description/*, TransactonTypeEnum SelectedTransaction, Decimal Amount, String TransactionID, DateTime Date, SortOrder SelectedSortOrder*/)
        {
            List<Transaction> SelectedTransactions = new List<Transaction>();
            List<Transaction> AllTransactions = db.Transactions.ToList();

            //start with db set with wanted data
            var query = from t in db.Transactions
                        select t;

            //code for textbox searching
            if (SearchString == null || SearchString == "") //they didn't select anything
            {
                //query = query
            }
            else //they picked something
            {
                query = query.Where(t => t.Description.Contains(SearchString) || t.TransactionID.Contains(SearchString));
            }

            ////Transaction drop down list
            //switch (SelectedTransaction)
            //{
            //    case TransactionType.All:
            //        //query = query 
            //        break;
            //    case TransactionType.Bill:
            //        query = query.Where(t => t.TransactionType == Transaction.TransactionTypeEnum.);
            //        break;
            //}

            //Code for radio buttons
            //switch (SelectedGender)
            //{
            //    case Gender.All:
            //        //query = query 
            //        break;
            //    case Gender.Male:
            //        query = query.Where(c => c.Gender == "Male");
            //        break;
            //    case Gender.Female:
            //        query = query.Where(c => c.Gender == "Female");
            //        break;
            //}

            //Code for desired sales textbox
            //check to see if string is valid
            //if ( Convert.ToString(Amount) == null || Convert.ToString(Amount) == "")
            //{
            //    //query = query 
            //}
            //else
            //{
            //    try
            //    {
            //        Amount = Convert.ToDecimal(Amount);
            //    }

            //    //code to display when something is wrong
            //    catch
            //    {
            //        //add message for viewbag
            //        ViewBag.Message = Amount + " is not a valid number. Please try again.";

            //        //re-populate dropdown
            //        ViewBag.TransactionType = GetAllTransactions();

            //        //send user back to advanced search pagee
            //        return View("Transactions/Details");
            //    }

            //radio buttons for specific parameters 
            //switch (Convert.ToString(Amount))
            //    {
            //        case :
            //        {
            //            query = query.Where(c => c.AverageSale >= decSalesAmount);
            //            break;
            //        }
            //        case CustomAmounts.FirstHundred:
            //        {
            //            query = query.Where(c => c.AverageSale <= decSalesAmount);
            //            break;
            //        }
            //        case CustomAmounts.FirstHundred:
            //        {
            //            query = query.Where(c => c.AverageSale <= decSalesAmount);
            //            break;
            //        }
            //        case CustomAmounts.FirstHundred:
            //        {
            //            query = query.Where(c => c.AverageSale <= decSalesAmount);
            //            break;
            //        }
            //   }
            //}

            // Transaction Date
            //if (daysBack == 0)
            //{
            //    // Display / add to view bag
            //    ViewBag.NumberSelectedTransactions = db.Transactions.ToList().Count;

            //    // Display list
            //    return View(db.Transactions.ToList());
            //}
            //else
            //{
            //    var oldestDate = (DateTime.Today).AddDays(Convert.ToDouble(daysBack) * -1);

            //    query = from t in db.Transactions
            //            where t.TransactionDate >= oldestDate
            //            select t;

            //}

            SelectedTransactions = query.ToList();

            //count # of records
            ViewBag.CustomerCount = SelectedTransactions.Count();
            ViewBag.TotalCustomerCount = AllTransactions.Count();

            //return limited queries
            return View("Transactions/Details", SelectedTransactions.OrderBy(t => t.TransactionID).ThenBy(t => t.TransactionType).ThenBy(t => t.Description).ThenBy(t => t.Amount));
        }




        public ActionResult Search() {
            // Kevin work here

            return View();
        }

        public ActionResult SearchResults() {
            // Kevin work here

            return View();
        }

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.BankAccount).Include(t => t.Disputes).Include(t => t.Payee);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }


        public ActionResult CreateTransaction()
        {
            // A view with 3 buttons, one to create withdrawal, deposit, and transfer
            return View();
        }

        //GET: Transactions/Deposit
        [HttpGet]
        public ActionResult CreateDeposit()
        {

            // We need a list of bank accounts to deposit to
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var accounts = from a in db.BankAccounts
                           where a.UserID.Equals(userId)
                           select a;
            ViewBag.BankAccountID = new SelectList(accounts, "BankAccountID", "Name");

            // The actual view to create a deposit
            return View();
        }
        //POST: Transactions/Deposit
        [HttpPost]
        public ActionResult CreateDeposit(Deposit deposit)
        {

            // Give this transaction the right primary key
            var idObject = db.Transactions.OrderByDescending(b => b.TransactionID).FirstOrDefault();
            if (idObject == null) deposit.TransactionID = "1000000000";
            else
            {
                int nextId = Convert.ToInt32(idObject.TransactionID) + 1;
                String nextIdString = nextId.ToString();
                deposit.TransactionID = nextIdString;
            }

            // Do validation here
            if (deposit.Amount <= 0) {
                return RedirectToAction("Error", "Home", new { ErrorMessage = "Please deposit an amount greater than 0." });
            }

            // Any changes
            if (deposit.Amount > 5000)
            {
                deposit.TransactionStatus = Transaction.TransactionStatusEnum.Pending;
            }
            else
            {
                deposit.TransactionStatus = Transaction.TransactionStatusEnum.Approved;

                // Must update the balance
                BankAccount BankAccount = db.BankAccounts.Find(deposit.BankAccountID);
                BankAccount.Balance = BankAccount.Balance + deposit.Amount;
                db.SaveChanges();
            }

            // Actually committing the deposit
            if (ModelState.IsValid)
            {
                db.Transactions.Add(deposit);
                db.SaveChanges();
                return RedirectToAction("Details", "BankAccount", new { BankAccountID = deposit.BankAccountID });
            }

            // We need a list of bank accounts to deposit to
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var accounts = from a in db.BankAccounts
                           where a.UserID.Equals(userId)
                           select a;
            ViewBag.BankAccountID = new SelectList(accounts, "BankAccountID", "Name");

            // The actual view to create a deposit
            return RedirectToAction("Details", "BankAccount", new { BankAccountID = deposit.BankAccountID });
        }

        //GET: Transactions/Withdrawal
        [HttpGet]
        public ActionResult CreateWithdrawal()
        {
            // We need a list of bank accounts to deposit to
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var accounts = from a in db.BankAccounts
                           where a.UserID.Equals(userId)
                           select a;
            ViewBag.BankAccountID = new SelectList(accounts, "BankAccountID", "Name");

            // The actual view to create withdrawal
            return View();
        }
        //POST: Transactions/Withdrawal
        [HttpPost]
        public ActionResult CreateWithdrawal(Withdrawal withdrawal)
        {
            // Give this transaction the right primary key
            var idObject = db.Transactions.OrderByDescending(b => b.TransactionID).FirstOrDefault();
            if (idObject == null) withdrawal.TransactionID = "1000000000";
            else
            {
                int nextId = Convert.ToInt32(idObject.TransactionID) + 1;
                String nextIdString = nextId.ToString();
                withdrawal.TransactionID = nextIdString;
            }

            // Do validation here
            if (withdrawal.Amount <= 0)
            {
                return RedirectToAction("Error", "Home", new { ErrorMessage = "Please deposit an amount greater than 0." });
            }

            // Any changes
            if (withdrawal.Amount > 1000)
            {
                withdrawal.TransactionStatus = Transaction.TransactionStatusEnum.Pending;
            }
            else
            {
                withdrawal.TransactionStatus = Transaction.TransactionStatusEnum.Approved;

                // Must update the balance
                BankAccount BankAccount = db.BankAccounts.Find(withdrawal.BankAccountID);
                BankAccount.Balance = BankAccount.Balance - withdrawal.Amount;
                db.SaveChanges();
            }

            // Actually committing the withdrawal
            if (ModelState.IsValid)
            {
                db.Transactions.Add(withdrawal);
                db.SaveChanges();
                return RedirectToAction("Details", "BankAccount", new { BankAccountID = withdrawal.BankAccountID });
            }

            // We need a list of bank accounts to deposit to
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var accounts = from a in db.BankAccounts
                           where a.UserID.Equals(userId)
                           select a;
            ViewBag.BankAccountID = new SelectList(accounts, "BankAccountID", "Name");

            // The actual view to create withdrawwal
            return RedirectToAction("Details", "BankAccount", new { BankAccountID = withdrawal.BankAccountID });
        }

        //GET: Transactions/Transfer
        [HttpGet]
        public ActionResult CreateTransfer()
        {
            // We need a list of bank accounts to transfer to and from
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var accounts = from a in db.BankAccounts
                           where a.UserID.Equals(userId)
                           select a;
            ViewBag.BankAccountID = new SelectList(accounts, "BankAccountID", "Name");
            ViewBag.ReceivingBankAccountID = new SelectList(accounts, "BankAccountID", "Name");

            // The actual view to create transfer
            return View();
        }
        //POST: Transactions/Transfer
        [HttpPost]
        public ActionResult CreateTransfer(Transfer transfer)
        {
            // Give this transaction the right primary key
            var idObject = db.Transactions.OrderByDescending(b => b.TransactionID).FirstOrDefault();
            if (idObject == null) transfer.TransactionID = "1000000000";
            else
            {
                int nextId = Convert.ToInt32(idObject.TransactionID) + 1;
                String nextIdString = nextId.ToString();
                transfer.TransactionID = nextIdString;
            }

            // Do validation here
            if (transfer.Amount <= 0)
            {
                return RedirectToAction("Error", "Home", new { ErrorMessage = "Please deposit an amount greater than 0." });
            }

            // Any changes
            if (transfer.Amount > 1000)
            {
                transfer.TransactionStatus = Transaction.TransactionStatusEnum.Pending;
            }
            else
            {
                transfer.TransactionStatus = Transaction.TransactionStatusEnum.Approved;

                // Must update the balance
                BankAccount BankAccount = db.BankAccounts.Find(transfer.BankAccountID);
                BankAccount.Balance = BankAccount.Balance - transfer.Amount;
                db.SaveChanges();

                // Of the receiving account too...
                BankAccount ReceivingBankAccount = db.BankAccounts.Find(transfer.ReceivingBankAccountID);
                ReceivingBankAccount.Balance = ReceivingBankAccount.Balance + transfer.Amount;
                db.SaveChanges();
            }

            // Actually committing the withdrawal
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transfer);
                db.SaveChanges();
                return RedirectToAction("Details", "BankAccount", new { BankAccountID = transfer.BankAccountID });
            } else
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine("Error: " + error.ErrorMessage);
                    }
                }
                return RedirectToAction("Error", "Home", new { ErrorMessage = "Something went wrong creating your transfer." });
            }

        }

        [HttpGet]
        public ActionResult CreatePayment()
        {

            // The actual view to create transfer
            return View();
        }
        //POST: Transactions/Deposit
        [HttpPost]
        public ActionResult CreatePayment(Payment payment, Payee payee)
        {

            // The actual view to create transfer
            return View();
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.BankAccountID = new SelectList(db.BankAccounts, "BankAccountID", "Name");
            ViewBag.TransactionID = new SelectList(db.Disputes, "TransactionID", "DisputeID");
            ViewBag.PayeeID = new SelectList(db.Payees, "PayeeID", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionID,TransactionType,Description,Amount,Comments,Date,BankAccountID,PayeeID")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BankAccountID = new SelectList(db.BankAccounts, "BankAccountID", "Name", transaction.BankAccountID);
            ViewBag.TransactionID = new SelectList(db.Disputes, "TransactionID", "DisputeID", transaction.TransactionID);
            ViewBag.PayeeID = new SelectList(db.Payees, "PayeeID", "Name", transaction.PayeeID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankAccountID = new SelectList(db.BankAccounts, "BankAccountID", "Name", transaction.BankAccountID);
            ViewBag.TransactionID = new SelectList(db.Disputes, "TransactionID", "DisputeID", transaction.TransactionID);
            ViewBag.PayeeID = new SelectList(db.Payees, "PayeeID", "Name", transaction.PayeeID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,TransactionType,Description,Amount,Comments,Date,BankAccountID,PayeeID")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankAccountID = new SelectList(db.BankAccounts, "BankAccountID", "Name", transaction.BankAccountID);
            ViewBag.TransactionID = new SelectList(db.Disputes, "TransactionID", "DisputeID", transaction.TransactionID);
            ViewBag.PayeeID = new SelectList(db.Payees, "PayeeID", "Name", transaction.PayeeID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
